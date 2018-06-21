using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Celeste_AOEO_Controls.Properties;

namespace Celeste_AOEO_Controls.Helpers
{
    public class SkinHelperFonts
    {
        private static readonly PrivateFontCollection Pfc;

        [DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont, IntPtr pdv,
            [In] ref uint pcFonts);

        static SkinHelperFonts()
        {
            //Create your private font collection object.
            Pfc = new PrivateFontCollection();

            //Select your font from the resources.            
            var fontLength = Resources.Ashley_Crawford_CG_1_TTF.Length;

            // create a buffer to read in to
            var fontdata = Resources.Ashley_Crawford_CG_1_TTF;

            // create an unsafe memory block for the font data
            var data = Marshal.AllocCoTaskMem(fontLength);

            // copy the bytes to the unsafe memory block
            Marshal.Copy(fontdata, 0, data, fontLength);

            uint cFonts = 0;
            AddFontMemResourceEx(data, (uint) fontLength, IntPtr.Zero, ref cFonts);

            //pass the font to the font collection
            Pfc.AddMemoryFont(data, fontLength);

            Marshal.FreeCoTaskMem(data);
        }

        public static void SetFont(Control.ControlCollection controls)
        {
            try
            {
                if (controls == null)
                    return;

                foreach (Control c in controls)
                    if (c is TextBox)
                    {
                        c.Font = new Font("Arial", c.Font.Size, c.Font.Style);
                    }
                    else if (c is RichTextBox)
                    {
                        c.Font = new Font("Arial", c.Font.Size, c.Font.Style);
                    }
                    else
                    {
                        c.Font = new Font(Pfc.Families[0], c.Font.Size, c.Font.Style);
                        if (c.Controls.Count > 0)
                            SetFont(c.Controls);
                    }
            }
            catch (Exception)
            {
                //
            }
        }
    }
}