using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClarinetTraining
{

    public class Note
    {
        public int note;
        public int octave;
        public bool sus;
        public bool bmol;

        public static string[] notesKey = new string[] { "C", "D", "E", "F", "G", "A", "B" };
        public static string[] notesName = new string[] { "Do", "Re", "Mi", "Fa", "Sol", "La", "Si" };

        public Note()
        {
        }

        public Note(string str)
        {
        }

        public Note(int note, bool sus = false, bool bmol = false, int scale = 0)
        {
            this.note = note;
            this.sus = sus;
            this.bmol = bmol;
            this.octave = scale;
        }


        public string ToString(string format)
        {

            return notesKey[note] + (sus ? "#" : "") + (bmol ? "b" : "") + (octave+1).ToString() ;

        }
        public override string ToString()
        {

            return this.ToString("");

        }

        public Note normalize()
        {
            var n = new Note(note, sus, bmol, octave);

            if (n.bmol)
            {
                n.note--;
                n.bmol = false;
                n.sus = true;

                if (n.note < 0)
                {
                    n.note = 6;
                    n.octave--;
                }
            }

            if (n.note == 2 && n.sus)
            {
                n.note = 3;
                n.sus = false;
            }
            if (n.note == 6 && n.sus)
            {
                n.note = 0;
                n.sus = false;
                n.octave++;
            }

            return n;
        }
    }

   
}
