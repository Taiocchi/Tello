# Tello AI
⚠️ I prerequisiti scritti di seguito sono fondamentali per il corretto funzionamento. Si prega di porre attenzione
**Tello AI** è il software che ti permette di controllare un drone DJI Tello direttamente dal tuo portatile.  
 Ma non finisce qui: grazie all’integrazione con modelli di intelligenza artificiale, le possibilità diventano praticamente infinite e si adattano alle tue esigenze.

Vuoi farlo muovere in strada? Può riconoscere i principali cartelli segnaletici.  
 Hai un allevamento? Può sorvolarlo e identificare gli animali sottostanti.

**Tello AI** è il tuo assistente nei piccoli compiti quotidiani, aiutandoti a monitorare ciò che ti circonda… **senza doverti muovere**. Comodo, vero? 🙂

Nota: il programma lavora con un solo modello alla volta, quanto sopra sono esempi di un possibile funzionamento. Si consiglia di utilizzare [Teachable Machine](https://teachablemachine.withgoogle.com/) per addestrare la propria piccola rete neurale da implementare sul drone.

### Funzionalità principali

* Gestione dei movimenti di base tramite interfaccia form di Windows  
* Possibilità di caricare ed eseguire percorsi predefiniti, creati dall’utente  
* Visualizzazione dello streaming video in tempo reale su una seconda finestra (in modo da poter usare comodamente anche 2 schermi)  
* Analisi tramite modelli di object detection dello streaming video

### Prerequisiti

1. Inserire i file .exe di [ffmpeg](https://ffmpeg.org/download.html) ed [MJPEGServer](https://github.com/blueimp/mjpeg-server) nella directory bin\>Debug di Tello e TelloCamera (senza rinominare)  
2. Installare i pacchetti Nuget richiesti da Tello e TelloCamera:  
   1. MjpegProcessor  
   2. Newtonsoft.Json  
   3. NLog

Nota: Quando si avvia la telecamera il software avvia l’esecuzione dell’eseguibile del progetto dedicato a quest’ultima funzionalità, il percorso è a priori impostato come dinamico, ma se questo viene spostato lo streaming non funzionerà.

### Utilizzo

Dopo la prima compilazione, avviare l’eseguibile Tello (in genere si trova in bin\>Debug). Buon divertimento\!

### Crediti

Luca Taiocchi  
Mauro Vecchi  
Michele Colombo  
Gabriele Mazzoleni  
Alessandro Nodari
