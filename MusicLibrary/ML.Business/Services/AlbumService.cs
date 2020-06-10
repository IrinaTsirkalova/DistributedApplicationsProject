using ML.Business.DTOs;
using ML.Data;
using ML.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ML.Business.Services
{
    public class AlbumService
    {
        public IEnumerable<AlbumDto> GetAllAlbumsByTitle(string albumTitle)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                var albums = unitOfWork.AlbumRepository.GetAll(a => a.AlbumTitle.Contains(albumTitle));

                var result = albums.Select(album => new AlbumDto
                {
                    Id = album.Id,
                    AlbumTitle = album.AlbumTitle,
                    AlbumDescription = album.AlbumDescription,
                    AlbumReleaseDate = album.AlbumReleaseDate,
                    AlbumNumberOfSongs = album.AlbumNumberOfSongs,
                    AlbumPrice = album.AlbumPrice,
                    AlbumRating = album.AlbumRating,
                    ArtistId = album.ArtistId,
                    Artist = new ArtistDto
                    {
                        Id = album.ArtistId,
                        FName = album.Artist.FName,
                        LName = album.Artist.LName,
                        Gender = album.Artist.Gender,
                        Birthdate = album.Artist.Birthdate,
                        ArtistRating = album.Artist.ArtistRating,
                        NumberOfSongsProduced = album.Artist.NumberOfSongsProduced,
                        CurrentLabel = album.Artist.CurrentLabel
                    },
                    GenreId = album.GenreId,
                    Genre = new GenreDto
                    {
                        Id = album.GenreId,
                        GenreName = album.Genre.GenreName,
                        GenreDescription = album.Genre.GenreDescription,
                        GenreCountryFounder = album.Genre.GenreCountryFounder,
                        GenreSongAvgLength = album.Genre.GenreSongAvgLength,
                        GenreYearFounded = album.Genre.GenreYearFounded
                    },
                    SongId = album.SongId,
                    Song = new SongDto
                    {
                        Id = album.SongId,
                        SongTitle = album.Song.SongTitle,
                        SongDuration = album.Song.SongDuration,
                        SongReleasedOn = album.Song.SongReleasedOn,
                        SongRating = album.Song.SongRating,
                        ArtistId = album.Song.ArtistId,
                        GenreId = album.Song.GenreId
                    }
                }).ToList();

                return result;
            }
        }

        public IEnumerable<AlbumDto> GetAll()
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                var albums = unitOfWork.AlbumRepository.GetAll();

                var result = albums.Select(album => new AlbumDto
                {
                    Id = album.Id,
                    AlbumTitle = album.AlbumTitle,
                    AlbumDescription = album.AlbumDescription,
                    AlbumReleaseDate = album.AlbumReleaseDate,
                    AlbumNumberOfSongs = album.AlbumNumberOfSongs,
                    AlbumPrice = album.AlbumPrice,
                    AlbumRating = album.AlbumRating,
                    ArtistId = album.ArtistId,
                    Artist = new ArtistDto
                    {
                        Id = album.ArtistId,
                        FName = album.Artist.FName,
                        LName = album.Artist.LName,
                        Gender = album.Artist.Gender,
                        Birthdate = album.Artist.Birthdate,
                        ArtistRating = album.Artist.ArtistRating,
                        NumberOfSongsProduced = album.Artist.NumberOfSongsProduced,
                        CurrentLabel = album.Artist.CurrentLabel
                    },
                    GenreId = album.GenreId,
                    Genre = new GenreDto
                    {
                        Id = album.GenreId,
                        GenreName = album.Genre.GenreName,
                        GenreDescription = album.Genre.GenreDescription,
                        GenreCountryFounder = album.Genre.GenreCountryFounder,
                        GenreSongAvgLength = album.Genre.GenreSongAvgLength,
                        GenreYearFounded = album.Genre.GenreYearFounded
                    },
                    SongId = album.SongId,
                    Song = new SongDto
                    {
                        Id = album.SongId,
                        SongTitle = album.Song.SongTitle,
                        SongDuration = album.Song.SongDuration,
                        SongReleasedOn = album.Song.SongReleasedOn,
                        SongRating = album.Song.SongRating,
                        ArtistId = album.Song.ArtistId,
                        GenreId = album.Song.GenreId
                    }
                }).ToList();
                return result;
            }
        }

        public AlbumDto GetById(int id)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                var album = unitOfWork.AlbumRepository.GetById(id);

                return album == null ? null : new AlbumDto
                {
                    Id = album.Id,
                    AlbumTitle = album.AlbumTitle,
                    AlbumDescription = album.AlbumDescription,
                    AlbumReleaseDate = album.AlbumReleaseDate,
                    AlbumNumberOfSongs = album.AlbumNumberOfSongs,
                    AlbumPrice = album.AlbumPrice,
                    AlbumRating = album.AlbumRating,
                    ArtistId = album.ArtistId,
                    Artist = new ArtistDto
                    {
                        Id = album.ArtistId,
                        FName = album.Artist.FName,
                        LName = album.Artist.LName,
                        Gender = album.Artist.Gender,
                        Birthdate = album.Artist.Birthdate,
                        ArtistRating = album.Artist.ArtistRating,
                        NumberOfSongsProduced = album.Artist.NumberOfSongsProduced,
                        CurrentLabel = album.Artist.CurrentLabel
                    },
                    GenreId = album.GenreId,
                    Genre = new GenreDto
                    {
                        Id = album.GenreId,
                        GenreName = album.Genre.GenreName,
                        GenreDescription = album.Genre.GenreDescription,
                        GenreCountryFounder = album.Genre.GenreCountryFounder,
                        GenreSongAvgLength = album.Genre.GenreSongAvgLength,
                        GenreYearFounded = album.Genre.GenreYearFounded
                    },
                    SongId = album.SongId,
                    Song = new SongDto
                    {
                        Id = album.SongId,
                        SongTitle = album.Song.SongTitle,
                        SongDuration = album.Song.SongDuration,
                        SongReleasedOn = album.Song.SongReleasedOn,
                        SongRating = album.Song.SongRating,
                        ArtistId = album.Song.ArtistId,
                        GenreId = album.Song.GenreId
                    }
                };
            }
        }

        public bool Create(AlbumDto albumDto)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                var album = new Album()
                {
                    AlbumTitle = albumDto.AlbumTitle,
                    AlbumDescription = albumDto.AlbumDescription,
                    AlbumReleaseDate = albumDto.AlbumReleaseDate,
                    AlbumNumberOfSongs = albumDto.AlbumNumberOfSongs,
                    AlbumPrice = albumDto.AlbumPrice,
                    AlbumRating = albumDto.AlbumRating,
                    ArtistId = albumDto.ArtistId,
                    GenreId = albumDto.GenreId,
                    SongId = albumDto.SongId,
                    CreatedOn = DateTime.Now
                };

                unitOfWork.AlbumRepository.Create(album);
                return unitOfWork.Save();
            }
        }

        public bool Update(AlbumDto albumDto)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                var result = unitOfWork.AlbumRepository.GetById(albumDto.Id);

                if (result == null)
                {
                    return false;
                }
                result.AlbumTitle = albumDto.AlbumTitle;
                result.AlbumDescription = albumDto.AlbumDescription;
                result.AlbumReleaseDate = albumDto.AlbumReleaseDate;
                result.AlbumNumberOfSongs = albumDto.AlbumNumberOfSongs;
                result.AlbumPrice = albumDto.AlbumPrice;
                result.AlbumRating = albumDto.AlbumRating;
                result.ArtistId = albumDto.ArtistId;
                result.GenreId = albumDto.GenreId;
                result.SongId = albumDto.SongId;
                result.UpdatedOn = DateTime.Now;

                unitOfWork.AlbumRepository.Update(result);

                return unitOfWork.Save();
            }
        }

        public bool Delete(int id)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                Album result = unitOfWork.AlbumRepository.GetById(id);
                if (result == null)
                {
                    return false;
                }

                unitOfWork.AlbumRepository.Delete(result);
                return unitOfWork.Save();
            }
        }

    }
}
