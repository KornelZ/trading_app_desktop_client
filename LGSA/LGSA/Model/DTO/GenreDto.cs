using LGSA.Model.ModelWrappers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LGSA_Server.Model.DTO
{
    public class GenreDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }

        public GenreWrapper createGenre()
        {
            GenreWrapper genre = new GenreWrapper(new LGSA.Model.dic_Genre());
            genre.GenreDescription = this.Description;
            genre.Id = this.Id;
            genre.Name = this.Name;
            return genre;
        }
    }
}