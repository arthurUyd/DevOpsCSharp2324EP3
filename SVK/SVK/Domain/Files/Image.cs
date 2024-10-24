﻿using Ardalis.GuardClauses;
using SVK.Domain.Common;
using HeyRed.Mime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVK.Domain.Files
{
    public class Image: ValueObject
    {
        public Uri BasePath { get; }
        public Guid Identifier { get; }
        public string Extension { get; }

        public string Filename => $"{Identifier}.{Extension}";
        public Uri FileUri => new Uri($"{BasePath}/{Filename}");

        public Image(Uri basePath, string contentType)
        {
            Identifier = Guid.NewGuid();
            Extension = MimeTypesMap.GetExtension(contentType).ToLower();
            BasePath = Guard.Against.Null(basePath, nameof(basePath));
        }

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Extension.ToLower();
            yield return Identifier;
            yield return BasePath;
        }

  
    }
}
