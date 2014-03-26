using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace HelloJCE.Helpers
{
    public static class ImageHelper
    {
        public static string Image(this HtmlHelper helper, string url)
        {
            var writer = new HtmlTextWriter(new StringWriter());

            writer.AddAttribute(HtmlTextWriterAttribute.Src, url);
            writer.RenderBeginTag(HtmlTextWriterTag.Img);
            writer.RenderEndTag();

            return writer.InnerWriter.ToString();
        }

        public static class ImageHelper
        {
            public static MvcHtmlString Image(this HtmlHelper helper, string src, string altText, string height)
            {
                var builder = new TagBuilder("img");
                builder.MergeAttribute("src", src);
                builder.MergeAttribute("alt", altText);
                builder.MergeAttribute("height", height);
                return MvcHtmlString.Create(builder.ToString(TagRenderMode.SelfClosing));
            }
        }
    }
}