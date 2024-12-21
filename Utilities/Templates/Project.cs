namespace RoXMX.Utilities.Templates
{
    internal class Project
    {
        static string ProjectName = "N/A";

        public static string Content = $@"{{
	""name"": ""{ProjectName}"",
	""tree"": {{
		""$className"": ""DataModel"",

		""Workspace"": {{
			""{ProjectName}"": {{
				""$path"": ""src""
			}}
		}}
	}}
}}
";

        public static string SetContext(string project)
        {
            ProjectName = project;
            return $@"{{
	""name"": ""{ProjectName}"",
	""tree"": {{
		""$className"": ""DataModel"",

		""Workspace"": {{
			""{ProjectName}"": {{
				""$path"": ""src""
			}}
		}}
	}}
}}
"; ;
        }
    }
}
