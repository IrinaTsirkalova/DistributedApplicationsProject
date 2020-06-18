using ML.Business.DTOs;
using ML.Data;
using ML.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ML.Business.Services
{
    public class SongService
    {
        public IEnumerable<SongDto> GetAllSongsByTitle(string songTitle)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                var songs = unitOfWork.SongRepository.GetAll(s => s.SongTitle.Contains(songTitle));

                var result = songs.Select(song => new SongDto
                {
                    Id = song.Id,
                    SongTitle = song.SongTitle,
                    SongDuration = song.SongDuration,
                    SongReleasedOn = song.SongReleasedOn,
                    SongRating = song.SongRating,
                    ArtistId = song.ArtistId,
                    Artist = new ArtistDto
                    {
                        Id = song.ArtistId,
                        FName = song.Artist.FName,
                        LName = song.Artist.LName,
                        Gender = song.Artist.Gender,
                        Birthdate = song.Artist.Birthdate,
                        ArtistRating = song.Artist.ArtistRating,
                        NumberOfSongsProduced = song.Artist.NumberOfSongsProduced,
                        CurrentLabel = song.Artist.CurrentLabel
                    },
                    GenreId = song.GenreId,
                    Genre = new GenreDto
                    {
                        Id = song.GenreId,
                        GenreName = song.Genre.GenreName,
                        GenreDescription = song.Genre.GenreDescription,
                        GenreCountryFounder = song.Genre.GenreCountryFounder,
                        GenreSongAvgLength = song.Genre.GenreSongAvgLength,
                        GenreYearFounded = song.Genre.GenreYearFounded
                    }
                }).ToList();

                return result;
            }
        }

        public IEnumerable<SongDto> GetAll()
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                var songs = unitOfWork.SongRepository.GetAll();

                var result = songs.Select(song => new SongDto
                {
                    Id = song.Id,
                    SongTitle = song.SongTitle,
                    SongDuration = song.SongDuration,
                    SongReleasedOn = song.SongReleasedOn,
                    SongRating = song.SongRating,
                    ArtistId = song.ArtistId,
                    Artist = new ArtistDto
                    {
                        Id = song.ArtistId,
                        FName = song.Artist.FName,
                        LName = song.Artist.LName,
                        Gender = song.Artist.Gender,
                        Birthdate = song.Artist.Birthdate,
                        ArtistRating = song.Artist.ArtistRating,
                        NumberOfSongsProduced = song.Artist.NumberOfSongsProduced,
                        CurrentLabel = song.Artist.CurrentLabel
                    },
                    GenreId = song.GenreId,
                    Genre = new GenreDto
                    {
                        Id = song.GenreId,
                        GenreName = song.Genre.GenreName,
                        GenreDescription = song.Genre.GenreDescription,
                        GenreCountryFounder = song.Genre.GenreCountryFounder,
                        GenreSongAvgLength = song.Genre.GenreSongAvgLength,
                        GenreYearFounded = song.Genre.GenreYearFounded

                    }
                }).ToList();
                return result;
            }
        }

        public SongDto GetById(int id)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                var song = unitOfWork.SongRepository.GetById(id);

                return song == null ? null : new SongDto
                {
                    Id = song.Id,
                    SongTitle = song.SongTitle,
                    SongDuration = song.SongDuration,
                    SongReleasedOn = song.SongReleasedOn,
                    SongRating = song.SongRating,
                    ArtistId = song.ArtistId,
                    Artist = new ArtistDto
                    {
                        Id = song.ArtistId,
                        FName = song.Artist.FName,
                        LName = song.Artist.LName,
                        Gender = song.Artist.Gender,
                        Birthdate = song.Artist.Birthdate,
                        ArtistRating = song.Artist.ArtistRating,
                        NumberOfSongsProduced = song.Artist.NumberOfSongsProduced,
                        CurrentLabel = song.Artist.CurrentLabel
                    },
                    GenreId = song.GenreId,
                    Genre = new GenreDto
                    {
                        Id = song.GenreId,
                        GenreName = song.Genre.GenreName,
                        GenreDescription = song.Genre.GenreDescription,
                        GenreCountryFounder = song.Genre.GenreCountryFounder,
                        GenreSongAvgLength = song.Genre.GenreSongAvgLength,
                        GenreYearFounded = song.Genre.GenreYearFounded
                    }
                };
            }
        }

        public bool Create(SongDto songDto)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                var song = new Song()
                {
                    SongTitle = songDto.SongTitle,
                    SongDuration = songDto.SongDuration,
                    SongReleasedOn = songDto.SongReleasedOn,
                    SongRating = songDto.SongRating,
                    ArtistId = songDto.ArtistId,
                    GenreId = songDto.GenreId,
                    CreatedOn = DateTime.Now
                };

                unitOfWork.SongRepository.Create(song);
                return unitOfWork.Save();
            }
        }

        public bool Update(SongDto songDto)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                var result = unitOfWork.SongRepository.GetById(songDto.Id);

                if (result == null)
                {
                    return false;
                }

                result.SongTitle = songDto.SongTitle;
                result.SongDuration = songDto.SongDuration;
                result.SongReleasedOn = songDto.SongReleasedOn;
                result.SongRating = songDto.SongRating;
                result.ArtistId = songDto.ArtistId;
                result.GenreId = songDto.GenreId;
                result.UpdatedOn = DateTime.Now;

                unitOfWork.SongRepository.Update(result);

                return unitOfWork.Save();
            }
        }

        public bool Delete(int id)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                Song result = unitOfWork.SongRepository.GetById(id);
                if (result == null)
                {
                    return false;
                }

                unitOfWork.SongRepository.Delete(result);
                return unitOfWork.Save();
            }
        }
    }
}
