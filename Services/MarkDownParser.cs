
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Html;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Westwind.AspNetCore.Markdown;
using Crowfounding.Models;

namespace Crowfounding.Data
{
    public static class MarkDownParser
    {
        static readonly string[] imageExtensions = { ".jpg", ".jpeg", ".png", ".svg", ".ico", ".bmp" };

        public static MarkupString Parse(string markdown)
        {
            return new MarkupString(Markdown.Parse(markdown));
        }
    }
}
