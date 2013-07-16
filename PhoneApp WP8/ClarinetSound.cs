using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace ClarinetTraining
{
    class ClarinetSound
    {
        SoundEffect effect;
        public  void playNote(Note n)
        {
            n = n.normalize();
            playNote(n.ToString());
        }

        public  void playNote(String noteId)
        {
            playSound(@"Assets\Sounds\" + noteId + ".wav");
        }

        private  void playSound(string path)
        {
            if (!string.IsNullOrEmpty(path))
            {
                using (var stream = TitleContainer.OpenStream(path))
                {
                    if (stream != null)
                    {
                        if(effect!=null)effect.Dispose();
                        effect = SoundEffect.FromStream(stream);
                        FrameworkDispatcher.Update();
                        effect.Play();
                    }
                }
            }
        }

    }
}
