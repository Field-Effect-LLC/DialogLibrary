using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Forms;
using DialogLibrary;
using System.Diagnostics;

namespace TestDialogLibrary
{
    [TestClass]
    public class UnitTest1
    {
        //string sampleText = "Lolor Ipsum Dolor Shmolor Folor Color Lolor Ipsum Dolor Shmolor Folor Color Lolor Ipsum Dolor Shmolor Folor Color Lolor Ipsum Dolor Shmolor Folor Color Lolor Ipsum Dolor Shmolor Folor Color Lolor Ipsum Dolor Shmolor Folor Color Lolor Ipsum Dolor Shmolor Folor Color Lolor Ipsum Dolor Shmolor Folor Color Lolor Ipsum Dolor Shmolor Folor Color Lolor Ipsum Dolor Shmolor Folor Color Lolor Ipsum Dolor Shmolor Folor Color Lolor Ipsum Dolor Shmolor Folor Color Lolor Ipsum Dolor Shmolor Folor Color Lolor Ipsum Dolor Shmolor Folor Color Lolor Ipsum Dolor Shmolor Folor Color Lolor Ipsum Dolor Shmolor Folor Color Lolor Ipsum Dolor Shmolor Folor Color Lolor Ipsum Dolor Shmolor Folor Color Lolor Ipsum Dolor Shmolor Folor Color Lolor Ipsum Dolor Shmolor Folor Color Lolor Ipsum Dolor Shmolor Folor Color Lolor Ipsum Dolor Shmolor Folor Color Lolor Ipsum Dolor Shmolor Folor Color Lolor Ipsum Dolor Shmolor Folor Color Lolor Ipsum Dolor Shmolor Folor Color Lolor Ipsum Dolor Shmolor Folor Color";
        //string sampleText = "A short message.";
        //string sampleText = "A short notification of things to come. Please click one of the buttons, and check the \"Don't Show this Again\" box, if you'd like not to see this box again.";
        string sampleCaption = "Lolor Ipsum Dolor Shmolor Folor";
        string sampleText = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vestibulum eget justo dui, vel dignissim felis. Donec interdum diam at ante auctor nec facilisis lorem sollicitudin. Donec facilisis, libero sed convallis euismod, eros turpis posuere urna, eget pellentesque lacus enim vel orci. Nam in ipsum nec felis blandit cursus. Sed tincidunt, odio id porta ultricies, turpis orci tristique nibh, nec condimentum sem augue eu sem. Vivamus sit amet vestibulum est. Nullam rhoncus felis vel nisi venenatis in ornare neque tempor. Proin vitae nibh vel sapien lobortis pellentesque. Suspendisse interdum consectetur metus nec pulvinar. Mauris eget sapien erat, vel pharetra lacus. In lobortis suscipit massa, eu dictum urna facilisis id. Quisque lacus magna, sollicitudin vitae rhoncus eu, cursus eget nunc. Integer quis semper mauris. Morbi sollicitudin, mi non euismod venenatis, ligula urna porttitor arcu, sed dapibus purus dolor eget ante. Nullam at odio leo.";
        [TestMethod]
        public void TestDialog()
        {
            //Test Default Button

            foreach (MessageBoxButtons buttons in Enum.GetValues(typeof(MessageBoxButtons)))
            foreach (MessageBoxIcon icon in Enum.GetValues(typeof( MessageBoxIcon)))
            //foreach (MessageBoxDefaultButton defaultButton in Enum.GetValues(typeof(MessageBoxDefaultButton)))
            {
                NotificationDialogResult result = NotificationDialog.Show(
                    sampleText,
                    sampleCaption,
                    buttons,
                    "Don't Show This Again",
                    icon);
                //MessageBox.Show(result.ToString());
                MessageBox.Show(sampleText, sampleCaption, buttons, icon);
                Debug.Print(result.ToString());
            }

            
        }   
    }
}
