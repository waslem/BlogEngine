﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogEngine.Core.Models
{
    public class CategoryImage
    {
        public int CategoryId { get; set; }
        public byte[] Image { get; set; }
    }
}
