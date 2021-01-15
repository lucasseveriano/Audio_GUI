using NAudio.CoreAudioApi;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Audio_GUI
{
    class AudioHandler
    {
        MMDeviceEnumerator enumerator;
        WaveStream mainOutputStream;
        WaveOutEvent player;

        public AudioHandler()
        {
            enumerator = new MMDeviceEnumerator();
        }        

        public MMDevice[] GetDevices() 
        {
            MMDeviceEnumerator deviceEnumerator = new MMDeviceEnumerator();
            var devices = deviceEnumerator.EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active);             
            return devices.ToArray();
        }

        public List<string> GetDeviceList()
        {
            List<string> list = new List<string>();

            for (int n = -1; n < WaveOut.DeviceCount; n++)
            {
                var caps = WaveOut.GetCapabilities(n);
                list.Add(caps.ProductName);
            }

            return list;
        }

        public void Play(String audiofile, int deviceNumber = -1)
        {
            mainOutputStream = new WaveFileReader(audiofile);
            WaveChannel32 volumeStream = new WaveChannel32(mainOutputStream);
            player = new WaveOutEvent();
            player.Init(volumeStream);
            player.DeviceNumber = deviceNumber;

            player.Play();

            while (player.PlaybackState == PlaybackState.Playing)
            {
                Thread.Sleep(1000);
            }

            //Thread t = new Thread(PlayThread);
            //t.Start();
        }

        public void PlayThread()
        {
            player.Play();

            while (player.PlaybackState == PlaybackState.Playing)
            {
                Thread.Sleep(1000);
            }
        }



        public int DeviceNameToNumber(string deviceName)
        {
            for (int n = 0; n <= WaveOut.DeviceCount; n++)
            {
                var caps = WaveOut.GetCapabilities(n);
                
                if (caps.ProductName.Equals(deviceName))
                {
                    return n;
                }
            }
            return -1;
        }

        public void Stop()
        {
            if(player == null)
            {
                return;
            }

            player.Stop();
            player.Dispose();
        }
    }
}
