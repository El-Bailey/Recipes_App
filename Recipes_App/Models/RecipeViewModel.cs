﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Recipes_App.Models
{
    public class RecipeViewModel
    {
        [Key]
        public int pkid { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Recipe { get; set; }
    }
}
