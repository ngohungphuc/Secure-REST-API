﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.API.Entities;
using Library.API.Models;

namespace Library.API.Services
{
    public class PropertyMappingService : IPropertyMappingService
    {
        private Dictionary<string, PropertyMappingValue> _authorPoPropertyMapping = new Dictionary<string, PropertyMappingValue>
        {
            {"Id", new PropertyMappingValue(new List<string>() {"Id"})} ,
            {"Genre", new PropertyMappingValue(new List<string>() {"Genre"})} ,
            {"Age", new PropertyMappingValue(new List<string>() {"DateOfBirth"},true)} ,
            {"Name", new PropertyMappingValue(new List<string>() {"FirstName","LastName"})}
        };

        private IList<IPropertyMapping> propertyMappings = new List<IPropertyMapping>();

        public PropertyMappingService()
        {
            propertyMappings.Add(new PropertyMapping<AuthorDto, Author>(_authorPoPropertyMapping));
        }

        public Dictionary<string, PropertyMappingValue> GetPropertyMapping<TSource, TDestination>()
        {
            //get matching mapping
            var matchingMapping = propertyMappings.OfType<PropertyMapping<TSource, TDestination>>();

            if (matchingMapping.Count() == 1)
            {
                return matchingMapping.First()._mappingDictionary;
            }

            throw new Exception($"Cant find exact property mapping instance for <{typeof(TSource)}>");
        }
    }
}