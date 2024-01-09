﻿using Lab_3.Core;
using Lab_3.Model;
namespace Lab_3.Musics;
public class MusicCatalog : IMusicCatalog
{
    private IMusicRepository _musics;
    public MusicCatalog(IMusicRepository musicRepository)
    {
        _musics = musicRepository;
    }


    public List<Music> listMusic()
    {
        Console.WriteLine("Все композиции в каталоге:");
        List<Music> musics = _musics.GetAll().Select(MusicModel => {
            return new Music(MusicModel.author, MusicModel.composition);
        }).ToList();

        foreach (var _music in musics)
        {
            Console.WriteLine(_music.getMusic());
        }

        return musics;
    }
    public List<Music> seachMusic(string PartOfName)
    {
        List<Music> resultMusic = _musics.FindByPartOfName(PartOfName);
        if (resultMusic.Count == 0)
            Console.WriteLine("Ничего не удовлетворяет критерию");
        else
        {
            Console.WriteLine("Результаты поиска:");
            foreach (var _music in resultMusic)
                Console.WriteLine(_music.getMusic());
        }
        return resultMusic;
    }
    public void addMusic(MusicModel music)
    {
        _musics.SetMusic(music);
    }
    public bool deleteMusic(string name)
    {
        List<Music> musics = _musics.GetAll().Select(MusicModel => {
            return new Music(MusicModel.author, MusicModel.composition);
        }).ToList();

        var find = false;
        for (var i = 0; i < musics.Count; i++)
        {
            if (musics[i].getMusic() == name)
            {
                Console.WriteLine($"Композиция '{name}' удалена.");
                _musics.DeleteMusic(name);
                return true;
            }
        }
        Console.WriteLine("Музыка не найдена");
        return false;
    }
}