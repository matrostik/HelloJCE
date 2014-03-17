using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace HelloJCE.Helpers
{
    public class ImageHelper
    {
        public static string Image(this HtmlHelper helper, string url)
        {
            var writer = new HtmlTextWriter(new StringWriter());

            writer.AddAttribute(HtmlTextWriterAttribute.Src, url);
            writer.RenderBeginTag(HtmlTextWriterTag.Img);
            writer.RenderEndTag();

            return writer.InnerWriter.ToString();
        }
    }
}