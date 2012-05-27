using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace TinyMCESupport
{
    public static class Helpers
    {
        public static HtmlString InitFullFeatured(UrlHelper url)
        {
            string customButtonImage = url.Content("~/Content/themes/admin/images/code.gif");

            string script = "<script type=\"text/javascript\">";
            script += "tinyMCE.init({";
            // General options
            script += "mode : \"textareas\",";
            script += "theme : \"advanced\",";
            script += "plugins : \"autolink,lists,spellchecker,pagebreak,style,layer,table,save,advhr,advimage,advlink,emotions,iespell,inlinepopups,insertdatetime,preview,media,searchreplace,print,contextmenu,paste,directionality,fullscreen,noneditable,visualchars,nonbreaking,xhtmlxtras,template\",";

            // Theme options
            script += "theme_advanced_buttons1 : \"save,newdocument,|,bold,italic,underline,strikethrough,|,justifyleft,justifycenter,justifyright,justifyfull,|,styleselect,formatselect,fontselect,fontsizeselect\",";
            script += "theme_advanced_buttons2 : \"cut,copy,paste,pastetext,pasteword,|,search,replace,|,bullist,numlist,|,outdent,indent,blockquote,|,undo,redo,|,link,unlink,anchor,image,cleanup,help,code,|,insertdate,inserttime,preview,|,forecolor,backcolor\",";
            script += "theme_advanced_buttons3 : \"tablecontrols,|,hr,removeformat,visualaid,|,sub,sup,|,charmap,emotions,iespell,media,advhr,|,print,|,ltr,rtl,|,fullscreen\",";
            script += "theme_advanced_buttons4 : \"insertlayer,moveforward,movebackward,absolute,|,styleprops,spellchecker,|,cite,abbr,acronym,del,ins,attribs,|,visualchars,nonbreaking,template,blockquote,pagebreak,|,insertfile,insertimage\",";
            script += "theme_advanced_toolbar_location : \"top\",";
            script += "theme_advanced_toolbar_align : \"left\",";
            script += "theme_advanced_statusbar_location : \"bottom\",";
            script += "theme_advanced_resizing : true,";
            script += "plugins : 'inlinepopups',";


            // Skin options
            script += "skin : \"o2k7\",";
            script += "skin_variant : \"silver\",";

            // Drop lists for link/image/media/template dialogs
            script += "template_external_list_url : \"js/template_list.js\",";
            script += "external_link_list_url : \"js/link_list.js\",";
            script += "external_image_list_url : \"js/image_list.js\",";
            script += "media_external_list_url : \"js/media_list.js\",";
            script += "width : \"600\",";
            script += "height : \"400\",";

            script += "});";
            script += "</script>";
            return new HtmlString(script);
        }

        public static HtmlString InitSimpleFeatured()
        {
            string script = "<script type=\"text/javascript\">";
            script += "tinyMCE.init({";
            // General options
            script += "mode : \"textareas\"";            
            script += "});";
            script += "</script>";
            return new HtmlString(script);
        }
    }
}
