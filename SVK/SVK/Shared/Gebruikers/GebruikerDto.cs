﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace SVK.Shared.Gebruikers;

public abstract class GebruikerDto
{
    public class Index
    {
        public int Id { get; set; }
        public string? Naam { get; set; }
    }

 
}
