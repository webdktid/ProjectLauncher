using System.Configuration;
using System.Diagnostics;
using LibGit2Sharp;

namespace ProjectLaunch
{
    public partial class FormMain : Form
    {
        private List<GitRepoData> GitRepoDataList;
        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            UpdateDataList();
            UpdateView();
        }

        private void UpdateView()
        {
            listviewOverview.Items.Clear();

            foreach (var gitRepoData in GitRepoDataList)
            {
                var listViewItem = listviewOverview.Items.Add(gitRepoData.Folder);
                listViewItem.SubItems.Add(gitRepoData.SolutionName);
                listViewItem.Tag = gitRepoData;


                listViewItem.SubItems.Add(gitRepoData.GitBranchName);



                if (gitRepoData.IsDirty)
                {
                    listViewItem.SubItems.Add("changes");
                    listViewItem.BackColor = Color.DarkSalmon;
                }
                else
                {
                    listViewItem.SubItems.Add("");
                    listViewItem.BackColor = Color.LightGreen;

                }

                listViewItem.SubItems.Add(gitRepoData.GitCommitMessage);
                listViewItem.SubItems.Add(gitRepoData.GitCommitAuthor);
                listViewItem.SubItems.Add(gitRepoData.GitRemoteChanges.ToString());

                if (gitRepoData.GitRemoteChanges > 0)
                    listViewItem.BackColor = Color.SkyBlue;

            }


            listviewOverview.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        }

  

        private void UpdateDataList()
        {
            GitRepoDataList = new List<GitRepoData>();

            string? baseDirectory = ConfigurationManager.AppSettings["directory"];

            if (baseDirectory  == null)
            {
                MessageBox.Show("startup directory not found in config", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }


            var directories = Directory.GetDirectories(baseDirectory, "*.*");
            directories.ToList().Sort();


            foreach (var directory in directories)
            {

                using (Repository repo = new Repository(directory))
                {

                    var trackingBranch = repo.Head.TrackedBranch;
                    var log = repo.Commits.QueryBy(new CommitFilter { IncludeReachableFrom = trackingBranch.Tip.Id, ExcludeReachableFrom = repo.Head.Tip.Id });

                    var gitRemoteChanges = log.Count();//Counts the number of log entries


                    StatusOptions options = new StatusOptions();
                    var repositoryStatus = repo.RetrieveStatus(options);

                    var commit = repo.Commits.FirstOrDefault();


                    GitRepoDataList.Add(new GitRepoData()
                    {
                        Folder = directory,
                        IsDirty = repositoryStatus.IsDirty,
                        GitCommitAuthor = commit.Author.Name,
                        GitCommitMessage = commit.MessageShort,
                        ProcessId = -1,
                        SolutionName = FindSolutionFile(directory),
                        GitBranchName = repo.Head.FriendlyName,
                        GitRemoteChanges = gitRemoteChanges
                    });

                }
            }

          
        }

        private string FindSolutionFile(string directory)
        {
            string solution = "";

            var file = Directory.GetFiles(directory, "*.sln").FirstOrDefault();
            if (file != null)
                return file;

            if (Directory.Exists(directory + "\\src"))
            {
                file = Directory.GetFiles(directory + "\\src", "*.sln").FirstOrDefault();
                if (file != null)
                    return file;
            }

            return solution;
        }

        private string GetSolutionNameFromFilename(string file)
        {
            if (file == "")
                return "";

            int ix = file.LastIndexOf("\\");
            return file.Substring(ix + 1, file.Length - ix + 1 - 6);
        }

        private void functionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateDataList();
            UpdateView();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void listViewOverview_DoubleClick(object sender, EventArgs e)
        {
            var ix = listviewOverview.SelectedIndices[0];

            if (ix == -1)
                return;

            var data = (GitRepoData) listviewOverview.Items[ix].Tag;

            if (string.IsNullOrWhiteSpace(data.SolutionName))
            {
                MessageBox.Show("Solution file not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

          //  GetLatest(data.Folder);


            var launcher = ConfigurationManager.AppSettings["launcher"];

            ProcessStartInfo startInfo = new ProcessStartInfo(launcher, data.SolutionName)
            {
                Verb = "runas"
            };
            var process = Process.Start(startInfo);
            
            if(process!=null)
                data.ProcessId = process.Id;

        }

      

   

    
   
    }
}