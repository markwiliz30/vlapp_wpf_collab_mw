using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using vlapp.Models;

namespace vlapp.Control
{
    internal class Helper
    {
        public void applySliderVal(object x, int val)
        {
            var z = (Slider)x;
            z.Value = val;
        }
        public void applyBlindProps(int x, List<BlindItem> blindItems, object[] sliderObjs)
        {
            BlindItem? tempBlind = new BlindItem();
            tempBlind = blindItems.Find(y => y.index == x);
            if (tempBlind != null)
            {
                applySliderVal(sliderObjs[0], tempBlind.currentGain);
                applySliderVal(sliderObjs[1], tempBlind.redVal);
                applySliderVal(sliderObjs[2], tempBlind.greenVal);
                applySliderVal(sliderObjs[3], tempBlind.blueVal);
            }
        }
        public void applyModuleProps(int bx, int mx, List<ModuleItem> moduleItems, object[] sliderObjs) 
        {
            ModuleItem? tempModule = new ModuleItem();
            tempModule = moduleItems.Find(y => y.blindIndex == bx && y.moduleIndex == mx);
            if (tempModule != null) 
            {
                applySliderVal(sliderObjs[0], tempModule.currentGain);
                applySliderVal(sliderObjs[1], tempModule.redVal);
                applySliderVal(sliderObjs[2], tempModule.greenVal);
                applySliderVal(sliderObjs[3], tempModule.blueVal);
            }
        }
        public int applyBlindConfig(string ip, int blindIndex, int redVal, int greenVal, int blueVal, int currVal)
        {
            //add send rgb and current gain to esp
            //save to database
            Database_Functions fDb = new Database_Functions();
            long x = fDb.blindConfig(ip, blindIndex, redVal, greenVal, blueVal, currVal);
            //9999 means data is existing
            if (x != 0)
            {
                //success
                return 1;
            }
            else
            {
                //failed
                return 0;
            }
        }

        public int applyModuleConfig(string ip, int blindIndex, int moduleIndex, int redVal, int greenVal, int blueVal, int currVal) 
        {
            //add send rgb and current gain to esp
            //save to database
            Database_Functions fDb = new Database_Functions();
            long x = fDb.moduleConfig(ip, blindIndex, moduleIndex, redVal, greenVal, blueVal, currVal);
            //9999 means data is existing
            if (x != 0)
            {
                //success
                return 1;
            }
            else 
            {
                //failed
                return 0;
            }
        }
    }
}
