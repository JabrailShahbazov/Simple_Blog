﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Blog.ViewModel
{
    public class PostViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string Body { get; set; } = "";

        public string Description { get; set; }= "";
        public string Tags { get; set; }= "";
        public string Category { get; set; }= "";

        public string CurrentImage { get; set; } = "";
        public IFormFile Image { get; set; } = null;
    }
}
