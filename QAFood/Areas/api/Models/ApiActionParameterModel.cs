﻿using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace QAFood.Areas.api.Models
{
    /// <summary>
    /// Query parameters for an api action.
    /// </summary>
    [NotMapped]
    public class ApiActionParameterModel
    {
        public string Name { get; set; }

        public string DefaultValue { get; set; }

        public Type Type { get; set; }

        public bool IsNullable { get; set; }

        public ApiActionParameterType ParameterType { get; set; }
    }
}
