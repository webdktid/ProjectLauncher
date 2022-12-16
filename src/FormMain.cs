using System.Configuration;
using System.Diagnostics;
using System.IO;
using LibGit2Sharp;
using static System.Windows.Forms.Design.AxImporter;

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

            this.Show();
            Application.DoEvents();

            UpdateDataList();
            Application.DoEvents();

            UpdateView();
            Application.DoEvents();
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

            int total = 0;
            foreach (ColumnHeader header in listviewOverview.Columns)
            {
                total += header.Width;
            }

            Width = total + 50;
        }


        void ShowInfo(string text)
        {
            labelInfo.Text = text;
            labelInfo.Update();
        }


        private void UpdateDataList()
        {
            GitRepoDataList = new List<GitRepoData>();

            string? baseDirectory = ConfigurationManager.AppSettings["directory"];

            if (baseDirectory == null)
            {
                MessageBox.Show("startup directory not found in config", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                Close();
            }


            var directories = Directory.GetDirectories(baseDirectory, "*.*");
            directories.ToList().Sort();

            int ix = 0;
            foreach (var directory in directories)
            {

                using (Repository repo = new Repository(directory))
                {

                    ix++;
                    ShowInfo($"Checking {directory} ({ix}/{directories.Length})");

                    CheckGitDirectory(repo, directory);
                }
            }

            ShowInfo($"Ready");

        }

        private void CheckGitDirectory(Repository repo, string directory)
        {
            var trackingBranch = repo.Head.TrackedBranch;

            var gitRemoteChanges = -1;
            if (trackingBranch != null)
            {
                var log = repo.Commits.QueryBy(new CommitFilter
                    {IncludeReachableFrom = trackingBranch.Tip.Id, ExcludeReachableFrom = repo.Head.Tip.Id});

                gitRemoteChanges = log.Count(); //Counts the number of log entries
            }

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


        private void functionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateDataList();
            UpdateView();
            Width = listviewOverview.Width + 20;
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void listViewOverview_DoubleClick(object sender, EventArgs e)
        {
            LaunchVisualStudio();
        }


        private void LaunchVisualStudio()
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

            if (process != null)
                data.ProcessId = process.Id;

        }



        private void pullWithGitExtentionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var ix = listviewOverview.SelectedIndices[0];

            if (ix == -1)
                return;

            var data = (GitRepoData) listviewOverview.Items[ix].Tag;

            var gitExtentions = ConfigurationManager.AppSettings["GitExtentions"];

            ProcessStartInfo startInfo = new ProcessStartInfo(gitExtentions, $"browse {data.Folder}")
            {
                Verb = "runas",
                WorkingDirectory = data.Folder
            };

            var process = Process.Start(startInfo);
        }

        private void launchVisualStudioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LaunchVisualStudio();
        }

        private void explorerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var ix = listviewOverview.SelectedIndices[0];

            if (ix == -1)
                return;

            var data = (GitRepoData) listviewOverview.Items[ix].Tag;




            ProcessStartInfo startInfo = new ProcessStartInfo("explorer.exe", data.Folder)
            {
                Verb = "runas"
            };
            var process = Process.Start(startInfo);

            if (process != null)
                data.ProcessId = process.Id;
        }





        private void getLatestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var gitRepoData in GitRepoDataList)
            {
                ShowInfo($"Getting latest for {gitRepoData.SolutionName}");

                using (Repository repo = new Repository(gitRepoData.Folder))
                {
                    var buildSignature = repo.Config.BuildSignature(DateTimeOffset.Now);
                    var repositoryStatus = repo.RetrieveStatus(new StatusOptions());

                    if (!repositoryStatus.IsDirty)
                    {
                        Rebase(repo, buildSignature);

                    }
                }
            }

            UpdateDataList();
            UpdateView();

            ShowInfo("Done..");

        }

        public void Rebase(Repository repo, Signature signature)
        {
            var head = repo.Head;
            var tracked = head.TrackedBranch;


            var result = repo.Rebase.Start(head, tracked, null, new Identity(signature.Name, signature.Email),
                new RebaseOptions());
            if (result.Status != RebaseStatus.Complete)
            {
                repo.Rebase.Abort();
                ShowInfo($"Could not rebase {head.FriendlyName} onto {head.TrackedBranch.FriendlyName}");
            }

            if (result.CompletedStepCount > 0)
            {
                // One or more commits were rebased
            }
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void gitPullrebaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var ix = listviewOverview.SelectedIndices[0];

            if (ix == -1)
                return;

            var data = (GitRepoData) listviewOverview.Items[ix].Tag;

            if (data.IsDirty)
            {
                MessageBox.Show("Dirty git branch");
                return;
            }

            using (Repository repo = new Repository(data.Folder))
            {
                var remote = repo.Network.Remotes["origin"];
                var refSpecs = remote.FetchRefSpecs.Select(x => x.Specification);
                Commands.Fetch(repo, remote.Name, refSpecs, new FetchOptions {Prune = true}, string.Empty);

                var id = new Identity("name", "email");
                var opt = new RebaseOptions();
                var rebaseResult = repo.Rebase.Start(
                    branch: repo.Head,
                    upstream: repo.Head.TrackedBranch,
                    onto: null,
                    id,
                    opt
                );
            }
            
            UpdateView();
        }

        private void gitCreateBranchAndPullRequestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var ix = listviewOverview.SelectedIndices[0];

            if (ix == -1)
                return;

            var data = (GitRepoData)listviewOverview.Items[ix].Tag;



            using (Repository repo = new Repository(data.Folder))
            {
                var repositoryStatus = repo.RetrieveStatus(new StatusOptions());
                
                var newBranchForm = new NewBranch(repositoryStatus);
                newBranchForm.ShowDialog();
                if (newBranchForm.Commit != true)
                    return;

                var originalBranchName  = repo.Head.CanonicalName;

                var gitName = ConfigurationManager.AppSettings["GitName"];
                var gitEmail = ConfigurationManager.AppSettings["GitEmail"];

                //create a new branch
                var localBranch = repo.CreateBranch(newBranchForm.BranchName);
                Commands.Checkout(repo, localBranch);
                Commands.Stage(repo,"*");

                //commit to the new branch
                var author = new Signature(gitName, gitEmail, DateTime.Now);
                repo.Commit(newBranchForm.CommitMessage, author,author, new CommitOptions());
                var commit = repo.Commit(newBranchForm.CommitMessage, author, author, new CommitOptions { AllowEmptyCommit = true });

                var startInfo = new ProcessStartInfo("git.exe", $"push origin {newBranchForm.BranchName}")
                {
                    Verb = "runas",
                    WorkingDirectory = data.Folder,
                    WindowStyle = ProcessWindowStyle.Normal
                };
                var process = Process.Start(startInfo);

       
                Commands.Checkout(repo, originalBranchName);
            }




        }
    }
}