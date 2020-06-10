using ML.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ML.Data
{
    public class UnitOfWork : IDisposable
    {
        private readonly MusicLibraryDbContext dbContext;

        private BaseRepository<Artist> artistRepository;
        private BaseRepository<Genre> genreRepository;
        private BaseRepository<Song> songRepository;
        private BaseRepository<Album> albumRepository;

        private bool disposed = false;

        public UnitOfWork()
        {
            this.dbContext = new MusicLibraryDbContext();
            dbContext.Database.EnsureCreated();
        }
        public BaseRepository<Artist> ArtistRepository
        {
            get
            {
                if (this.artistRepository == null)
                {
                    this.artistRepository = new BaseRepository<Artist>(dbContext);
                }
                return artistRepository;
            }
        }

        public BaseRepository<Genre> GenreRepository
        {
            get
            {
                if (this.genreRepository == null)
                {
                    this.genreRepository = new BaseRepository<Genre>(dbContext);
                }
                return genreRepository;
            }
        }

        public BaseRepository<Song> SongRepository
        {
            get
            {
                if (this.songRepository == null)
                {
                    this.songRepository = new BaseRepository<Song>(dbContext);
                }
                return songRepository;
            }
        }

        public BaseRepository<Album> AlbumRepository
        {
            get
            {
                if (this.albumRepository == null)
                {
                    this.albumRepository = new BaseRepository<Album>(dbContext);
                }
                return albumRepository;
            }
        }
        public bool Save()
        {
            try
            {
                dbContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    dbContext.Dispose();
                }
                disposed = true;
            }
        }

    }
}
