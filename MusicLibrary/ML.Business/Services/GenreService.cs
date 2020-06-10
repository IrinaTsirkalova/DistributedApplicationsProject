using ML.Business.DTOs;
using ML.Data;
using ML.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ML.Business.Services
{
    public class GenreService
    {
        public IEnumerable<GenreDto> GetAllByGenreName(string genreName)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                var genres = unitOfWork.GenreRepository.GetAll(g => g.GenreName == genreName);

                return genres.Select(genre => new GenreDto
                {
                    Id = genre.Id,
                    GenreName = genre.GenreName,
                    GenreDescription = genre.GenreDescription,
                    GenreCountryFounder = genre.GenreCountryFounder,
                    GenreYearFounded = genre.GenreYearFounded,
                    GenreSongAvgLength = genre.GenreSongAvgLength

                });
            }
        }

        public IEnumerable<GenreDto> GetAll()
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                var genres = unitOfWork.GenreRepository.GetAll();

                return genres.Select(genre => new GenreDto
                {
                    Id = genre.Id,
                    GenreName = genre.GenreName,
                    GenreDescription = genre.GenreDescription,
                    GenreCountryFounder = genre.GenreCountryFounder,
                    GenreYearFounded = genre.GenreYearFounded,
                    GenreSongAvgLength = genre.GenreSongAvgLength
                });
            }
        }

        public GenreDto GetById(int id)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                var genre = unitOfWork.GenreRepository.GetById(id);

                return genre == null ? null : new GenreDto
                {
                    Id = genre.Id,
                    GenreName = genre.GenreName,
                    GenreDescription = genre.GenreDescription,
                    GenreCountryFounder = genre.GenreCountryFounder,
                    GenreYearFounded = genre.GenreYearFounded,
                    GenreSongAvgLength = genre.GenreSongAvgLength
                };

            }
        }
       
        public bool Create(GenreDto genreDto)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                var genre = new Genre()
                {
                    GenreName = genreDto.GenreName,
                    GenreDescription = genreDto.GenreDescription,
                    GenreCountryFounder = genreDto.GenreCountryFounder,
                    GenreYearFounded = genreDto.GenreYearFounded,
                    GenreSongAvgLength = genreDto.GenreSongAvgLength,
                    CreatedOn = DateTime.Now
                };

                unitOfWork.GenreRepository.Create(genre);

                return unitOfWork.Save();
            }
        }

        public bool Update(GenreDto genreDto)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                var result = unitOfWork.GenreRepository.GetById(genreDto.Id);

                if (result == null)
                {
                    return false;
                }
                result.GenreName = genreDto.GenreName;
                result.GenreDescription = genreDto.GenreDescription;
                result.GenreCountryFounder = genreDto.GenreCountryFounder;
                result.GenreYearFounded = genreDto.GenreYearFounded;
                result.GenreSongAvgLength = genreDto.GenreSongAvgLength;
                result.UpdatedOn = DateTime.Now;

                unitOfWork.GenreRepository.Update(result);

                return unitOfWork.Save();
            }
        }

        public bool Delete(int id)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                Genre result = unitOfWork.GenreRepository.GetById(id);
                if (result == null)
                {
                    return false;
                }

                unitOfWork.GenreRepository.Delete(result);
                return unitOfWork.Save();
            }
        }
    }
}
