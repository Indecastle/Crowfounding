
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
    public static class ChatHelper
    {
        static readonly string[] imageExtensions = { ".jpg", ".jpeg", ".png", ".svg", ".ico", ".bmp" };

        public static MarkupString Parse(string markdown)
        {
            return new MarkupString(Markdown.Parse(markdown));
        }
        public static MarkupString ConvertUrl(Comment message)
        {
            if (message.IsFile)
                if (imageExtensions.Contains(Path.GetExtension(message.FileName)))
                    return new MarkupString($"<img src=\"{message.Text}\">");
                else
                    return new MarkupString($"<a href=\"{message.Text}\">{message.FileName}</a>");
            else
                return new MarkupString(Markdown.Parse(message.Text));
            
            
        }
    }
}
