using AutoFixture;
using Lab_3.Core;
using Lab_3.Model;
using Lab_3.Musics;
using Moq;
using NSubstitute;

namespace TestLab_3;
public class UnitTest1
{
    private Fixture _fixture = new Fixture();
    [Fact]
    public void FunctionList_ReturnsExpected()
    {
        // создать данные
        var musics = _fixture.Build<MusicModel>()
            .With(x => x.author, "author")
            .With(x => x.composition, "composition").CreateMany().ToList();

        // смоделированная база данных
        var repo = Substitute.For<IMusicRepository>();
        repo.GetAll().Returns(musics);

        var musicService = new MusicCatalog(repo);
        var result = musicService.listMusic();

        Assert.NotNull(result);
        Assert.Equal(3, result.Count);
    }

    [Fact]
    public void FunctionSearch_ReturnsExpected()
    {
        // создать данные
        var musics = new List<MusicModel>
        {
            new MusicModel { author = "author1", composition = "composition1" },
            new MusicModel { author = "author2", composition = "composition2" },
            new MusicModel { author = "author3", composition = "composition3" }
        };

        // Mock repository
        var repo = Substitute.For<IMusicRepository>();
        repo.FindByPartOfName("composition1").Returns(musics
            .Where(m => m.composition.Contains("composition1"))
            .Select(m => new Music(m.author, m.composition))
            .ToList());

        var musicService = new MusicCatalog(repo);
        var result = musicService.seachMusic("composition1");


        Assert.NotNull(result);
        Assert.Equal(1, result.Count);
    }

    [Fact]
    public void FunctionAdd_ShouldAddNewMusicToDatabase()
    {
        // создать данные
        var musics = _fixture.Build<MusicModel>()
            .With(x => x.author, "author")
            .With(x => x.composition, "composition").CreateMany(3).ToList();

        // Mock repository
        var repo = Substitute.For<IMusicRepository>();
        repo.GetAll().Returns(musics);

        var musicService = new MusicCatalog(repo);

        var newMusicModel = new MusicModel { author = "author", composition = "composition" };
        musicService.addMusic(newMusicModel);

        var initialMusicList = repo.GetAll();
        var updatedMusicList = initialMusicList.Append(newMusicModel).ToList();
        repo.GetAll().Returns(updatedMusicList);
        var result = musicService.listMusic();

        Assert.NotNull(result);
        Assert.Equal(4, result.Count);
    }

    [Fact]
    public void FunctionDelete_ShouldDeteleMusicFromDatabase_ReturnTrue()
    {
        // создать данные
        var musics = new List<MusicModel> {
            new MusicModel { author = "author1", composition = "composition1" },
            new MusicModel { author = "author2", composition = "composition2" },
            new MusicModel { author = "author3", composition = "composition3" }
        };

        // Mock repository
        var repo = Substitute.For<IMusicRepository>();
        repo.GetAll().Returns(musics);

        var musicService = new MusicCatalog(repo);
        Assert.True(musicService.deleteMusic("author1 - composition1"));
    }
    [Fact]
    public void FunctionDelete_ShouldDeteleMusicFromDatabase_ReturnFalse()
    {
        // создать данные
        var musics = new List<MusicModel> {
            new MusicModel { author = "author1", composition = "composition1" },
            new MusicModel { author = "author2", composition = "composition2" },
            new MusicModel { author = "author3", composition = "composition3" }
        };

        // Mock repository
        var repo = Substitute.For<IMusicRepository>();
        repo.GetAll().Returns(musics);

        var musicService = new MusicCatalog(repo);
        Assert.False(musicService.deleteMusic("author - composition"));
    }
}