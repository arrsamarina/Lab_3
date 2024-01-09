using System;
namespace Lab_3.Musics;
public class Music
{
    public string authorName { get; set; }
    public string compositionName { get; set; }
    public Guid Id;
    public Music()
    {
        Console.WriteLine("Введите имя автора:");
        authorName = Console.ReadLine();
        Console.WriteLine("Введите название композиции:");
        compositionName = Console.ReadLine();
        Id = Guid.NewGuid();
    }
    public Music(string authorName, string compositionName)
    {
        Id = Guid.NewGuid();
        this.authorName = authorName;
        this.compositionName = compositionName;
    }
    public string getMusic()
    {
        return $"{authorName} - {compositionName}";
    }
}
