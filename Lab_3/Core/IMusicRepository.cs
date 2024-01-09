using System.Collections.Generic;
using Lab_3.Model;
using Lab_3.Musics;

namespace Lab_3.Core;


public interface IMusicRepository
{
    //вывожу информацию обо всех существующих в каталоге композициях
    public List<MusicModel> GetAll();
    //добавляю информацию о композиции в каталог
    public void SetMusic(MusicModel music);
    public List<Music> FindByPartOfName(string PartOfName);
    public void DeleteMusic(string title);
}
