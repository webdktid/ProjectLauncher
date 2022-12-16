using LibGit2Sharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectLaunch
{
    public partial class NewBranch : Form
    {
        public string CommitMessage { get; set; }
        public string BranchName { get; set; }
        public bool Commit { get; set; }


        public NewBranch(RepositoryStatus repositoryStatus)
        {
            InitializeComponent();
            var repositoryStatus1 = repositoryStatus;
            labelStatus.Text = $@"Added:{repositoryStatus1.Added.Count()}, Modified:{repositoryStatus1.Modified.Count()} Removed:{repositoryStatus1.Removed.Count()}";
        }

        private void buttonCansel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonCerate_Click(object sender, EventArgs e)
        {
            BranchName = textBoxBranchName.Text;
            CommitMessage = textBoxCommitMessage.Text;
            Commit = true;
            Close();
        }
    }
}
