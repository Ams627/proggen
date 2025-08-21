namespace Proggen.Generators.Common;

public class FileSpec
{
    public FileSpec(FileSpec other)
    {
        Pathname = other.Pathname;
        Contents = other.Contents;
    }
    public FileSpec() { }
    public virtual string Pathname { get; set; }
    public virtual string[] Contents { get; set; }
    public virtual bool GitAdd { get; set; } = true;
}
