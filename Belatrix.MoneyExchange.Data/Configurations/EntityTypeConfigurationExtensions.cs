﻿using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration.Configuration;

namespace Belatrix.MoneyExchange.Data.Configurations
{
    internal static class EntityTypeConfigurationExtensions
    {
        private const string Index = "Index";

        public static PrimitivePropertyConfiguration HasIndex(
            this PrimitivePropertyConfiguration configuration,
            string indexName)
        {
            return configuration.HasColumnAnnotation(
                Index,
                new IndexAnnotation(new IndexAttribute(indexName) { IsUnique = false }));
        }

        public static PrimitivePropertyConfiguration HasUniqueIndex(
            this PrimitivePropertyConfiguration configuration,
            string indexName)
        {
            return configuration.HasColumnAnnotation(
                Index,
                new IndexAnnotation(new IndexAttribute(indexName) { IsUnique = true }));
        }
    }
}