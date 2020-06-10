using ML.Business.DTOs;
using ML.Data;
using ML.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ML.Business.Services
{
    public class ArtistService
    {
        public IEnumerable<ArtistDto> GetAllByFirstName(string firstName = null)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                var artists = unitOfWork.ArtistRepository.GetAll(artist => artist.FName == firstName);

                return artists.Select(artist => new ArtistDto
                {
                    Id = artist.Id,
                    FName = artist.FName,
                    LName = artist.LName,
                    Gender = artist.Gender,
                    Birthdate = artist.Birthdate,
                    ArtistRating = artist.ArtistRating,
                    NumberOfSongsProduced = artist.NumberOfSongsProduced,
                    CurrentLabel = artist.CurrentLabel
                });
            }
        }

        public IEnumerable<ArtistDto> GetAllByLastName(string lastName = null)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                var artists = unitOfWork.ArtistRepository.GetAll(a => a.LName == lastName);

                return artists.Select(artist => new ArtistDto
                {
                    Id = artist.Id,
                    FName = artist.FName,
                    LName = artist.LName,
                    Gender = artist.Gender,
                    Birthdate = artist.Birthdate,
                    ArtistRating = artist.ArtistRating,
                    NumberOfSongsProduced = artist.NumberOfSongsProduced,
                    CurrentLabel = artist.CurrentLabel
                });
            }
        }

        public IEnumerable<ArtistDto> GetAll()
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                var artists = unitOfWork.ArtistRepository.GetAll();

                return artists.Select(artist => new ArtistDto
                {
                    Id = artist.Id,
                    FName = artist.FName,
                    LName = artist.LName,
                    Gender = artist.Gender,
                    Birthdate = artist.Birthdate,
                    ArtistRating = artist.ArtistRating,
                    NumberOfSongsProduced = artist.NumberOfSongsProduced,
                    CurrentLabel = artist.CurrentLabel
                });
            }
        }

        public ArtistDto GetById(int id)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                var artist = unitOfWork.ArtistRepository.GetById(id);

                return artist == null ? null : new ArtistDto
                {
                    Id = artist.Id,
                    FName = artist.FName,
                    LName = artist.LName,
                    Gender = artist.Gender,
                    Birthdate = artist.Birthdate,
                    ArtistRating = artist.ArtistRating,
                    NumberOfSongsProduced = artist.NumberOfSongsProduced,
                    CurrentLabel = artist.CurrentLabel
                };
            }
        }

        public bool Create(ArtistDto artistDto)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                var artist = new Artist()
                {
                    FName = artistDto.FName,
                    LName = artistDto.LName,
                    Gender = artistDto.Gender,
                    Birthdate = artistDto.Birthdate,
                    ArtistRating = artistDto.ArtistRating,
                    NumberOfSongsProduced = artistDto.NumberOfSongsProduced,
                    CurrentLabel = artistDto.CurrentLabel,
                    CreatedOn = DateTime.Now
                };

                unitOfWork.ArtistRepository.Create(artist);

                return unitOfWork.Save();
            }
        }

        public bool Update(ArtistDto artistDto)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                var result = unitOfWork.ArtistRepository.GetById(artistDto.Id);

                if (result == null)
                {
                    return false;
                }
                result.FName = artistDto.FName;
                result.LName = artistDto.LName;
                result.Gender = artistDto.Gender;
                result.Birthdate = artistDto.Birthdate;
                result.ArtistRating = artistDto.ArtistRating;
                result.NumberOfSongsProduced = artistDto.NumberOfSongsProduced;
                result.CurrentLabel = artistDto.CurrentLabel;
                result.UpdatedOn = DateTime.Now;

                unitOfWork.ArtistRepository.Update(result);

                return unitOfWork.Save();
            }
        }

        public bool Delete(int id)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                Artist result = unitOfWork.ArtistRepository.GetById(id);
                if (result == null)
                {
                    return false;
                }

                unitOfWork.ArtistRepository.Delete(result);
                return unitOfWork.Save();
            }
        }

    }
}
