namespace ProjectLaunch
{
    public class GitRepoData
    {
        public string SolutionName { get; set; }
        public string Folder { get; set; }
        public bool IsDirty { get; set; }
        public string GitCommitMessage { get; set; }
        public string GitCommitAuthor  { get; set; }
        public string GitBranchName { get; set; }
        public long ProcessId { get; set; }
        public int GitRemoteChanges { get; set; }

    }
}
