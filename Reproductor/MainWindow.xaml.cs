using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
//Librerias de NAudio (Corre el proyecto una vez para que se traiga las librerias)
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
//Clase nueva para el timer del programa 
using System.Windows.Threading;

namespace Reproductor
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer;

        //Lector de archivos
        AudioFileReader reader;
        //Comunicacion conla tarjeta de audio
        //Exclusivo para salidas
        WaveOut output;
        string path = Environment.CurrentDirectory;

        int i = 0;

        int[] Tiempo = { 4000, 50000, 6000, 7000, 8000,9000, 10000,11000 };
        string[] Letra = {"Yo tengo mi bases","en todo los paises","si tiro una bomba","no creo que avise",
                            "esto es blanco o negro","yo no veo grises","si no exploto todo" }; 

        public MainWindow()
        {
            InitializeComponent();

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(500);
            
            timer.Tick += Timer_Tick;
        }


        private void Timer_Tick(object sender, EventArgs e)
        {
           if(reader.CurrentTime.TotalMilliseconds > Tiempo[i])
            {
                Lyrics.Text = Letra[i];
                i++;
            }
        }

        private void BtnReproducir_Click(object sender, RoutedEventArgs e)
        {

            if (output != null && output.PlaybackState == PlaybackState.Paused)
            {
                //Retomo reproduccion
                output.Play();
                btnReproducir.IsEnabled = false;

            }
            else
            {
                Lyrics.Visibility = Visibility.Visible;
                btnReproducir.Visibility = Visibility.Hidden;
                
                    reader = new AudioFileReader(path + "/JustinBieber.mp3");
                    output = new WaveOut();
             

                    output.PlaybackStopped += Output_PlaybackStopped;
                    output.Init(reader);
                    output.Play();
                    timer.Start();

               
            }
        }

        private void Output_PlaybackStopped(object sender, StoppedEventArgs e)
        {
            reader.Dispose();
            output.Dispose();
            timer.Stop();
        }
    }
}
