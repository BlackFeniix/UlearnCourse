using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeometryTasks;

namespace GeometryPainting
{
    public static class SegmentExtencions
    {
        private static Dictionary<Segment, Color> dictionaryOfSegments=new Dictionary<Segment,Color>();
        public static Color GetColor(this Segment segment)
        {
            return dictionaryOfSegments.ContainsKey(segment) ? dictionaryOfSegments[segment] : Color.Black;
        }
        
        public static void SetColor(this Segment segment, Color segmentColor)
        {
            var segmentAlreadyInDictionary = dictionaryOfSegments.ContainsKey(segment);

            if (!segmentAlreadyInDictionary)
            {
                dictionaryOfSegments.Add(segment, segmentColor);
            }
            else if (segmentAlreadyInDictionary && !dictionaryOfSegments[segment].Equals(segmentColor))
            {
                dictionaryOfSegments[segment] = segmentColor;
            }        
        }
    }
    //Напишите здесь код, который заставит работать методы segment.GetColor и segment.SetColor
}