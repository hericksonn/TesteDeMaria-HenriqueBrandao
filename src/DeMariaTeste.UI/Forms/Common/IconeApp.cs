using System;
using System.Drawing;
using System.IO;
using System.Reflection;

namespace DeMariaTeste.UI.Forms.Common
{
    public static class IconeApp
    {
        private static readonly object _lock = new object();
        private static Image _image;
        private static Icon _icon;

        public static Image LogoImage
        {
            get
            {
                lock (_lock)
                {
                    if (_image == null)
                    {
                        var caminho = ResolverArquivo("logo.png");
                        if (caminho != null) _image = Image.FromFile(caminho);
                    }
                    return _image;
                }
            }
        }

        public static Icon LogoIcon
        {
            get
            {
                lock (_lock)
                {
                    if (_icon == null)
                    {
                        var caminho = ResolverArquivo("logo.ico");
                        if (caminho != null)
                        {
                            using (var fs = File.OpenRead(caminho))
                            {
                                _icon = new Icon(fs);
                            }
                        }
                    }
                    return _icon;
                }
            }
        }

        private static string ResolverArquivo(string nome)
        {
            try
            {
                var basePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty;
                var caminho = Path.Combine(basePath, "Assets", nome);
                return File.Exists(caminho) ? caminho : null;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
