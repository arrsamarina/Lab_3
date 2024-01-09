using Lab_3.Model;
using Lab_3.Musics;
using static Lab_3.Musics.EMusicFunctions;

namespace Lab_3.View;
public class View
{
    private readonly MusicCatalog _musicCatalog;
    public View(MusicCatalog _musicCatalog)
    {
        this._musicCatalog = _musicCatalog ?? throw new AggregateException();
        usage();
    }
    public void start()
    {
        while (true)
        {
            var command = setCommand(getCommand()); ;
            if (command.Equals(quit)) break;
            switch (command)
            {
                case EMusicFunctions.list:
                    _musicCatalog.listMusic();
                    break;
                case EMusicFunctions.search:
                    Console.WriteLine("Введите часть названия, чтобы найти композицию в каталоге:");
                    _musicCatalog.seachMusic(Console.ReadLine());
                    break;
                case EMusicFunctions.add:
                    Music music = new Music();
                    _musicCatalog.addMusic(new MusicModel()
                    {
                        author = music.authorName,
                        composition = music.compositionName,
                        Id = music.Id
                    });
                    break;
                case EMusicFunctions.del:
                    Console.WriteLine("Введите полное название композиции, которую вы хотите удалить:");
                    _musicCatalog.deleteMusic(Console.ReadLine());
                    break;
            }
        }
    }
    private string getCommand()
    {
        Console.WriteLine("Введите команду: ");
        return Console.ReadLine();
    }
    private EMusicFunctions setCommand(string command)
    {
        var cm = command switch
        {
            "list" => list,
            "search" => search,
            "add" => add,
            "del" => del,
            "quit" => quit,
            _ => throw new InvalidDataException(),
        };
        return cm;
    }
    private void usage()
    {
        Console.WriteLine("Применение: \n"
                          + "Тип команды: \n"
                          + "'list' вывод экран всего музыкального каталога \n"
                          + "'search' поиск композиций в каталоге \n"
                          + "'add' добавление новой композиции \n"
                          + "'del' удаление композиции из каталога \n"
                          + "'quit' выход");
    }
}
