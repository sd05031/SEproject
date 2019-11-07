namespace SEproject.Data
{
    public class File
    {
        public string Name { get; private set; }
        public int Is_directory { get; private set; }
        public string Is_directory_Value { get; private set; }
        public File(string name, int dir)
        {
            Name = name;
            Is_directory = dir;
            if ( dir == 1)
            {
                Is_directory_Value = "Directory";
            }
            else
            {
                Is_directory_Value = "File";
            }
        }
    }
}
