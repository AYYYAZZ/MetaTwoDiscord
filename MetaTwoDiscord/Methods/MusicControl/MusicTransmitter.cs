using System;
using System.Collections.Generic;
using System.Text;

namespace MetaTwoDiscord.Methods.MusicControl
{
    class MusicTransmitter
    {
        public void PlayMusic()
        {
            var url = "http://media.ch9.ms/ch9/2876/fd36ef30-cfd2-4558-8412-3cf7a0852876/AzureWebJobs103.mp3";
            using (var mf = new MediaFoundationReader(url))
            using (var wo = new WaveOutEvent())
            {
                wo.Init(mf);
                wo.Play();
                while (wo.PlaybackState == PlaybackState.Playing)
                {
                    Thread.Sleep(1000);
                }
            }
        }
    }
}
