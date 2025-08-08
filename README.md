🎣 Gold Miner – Hook Movement with ESP32 Button Control

📌 Deskripsi
Proyek ini adalah implementasi game Gold Miner di Unity dengan sistem kontrol hook (pengait) yang dapat bergerak otomatis bolak-balik, lalu menembak ke bawah saat pemain menekan tombol.
Awalnya kontrol dilakukan dengan klik mouse, kemudian diintegrasikan dengan ESP32 dan push button untuk memberikan pengalaman bermain yang lebih interaktif dengan kontrol fisik.

🎮 Mekanisme Gameplay
Hook bergerak otomatis ke kiri-kanan dalam batas sudut tertentu.

Saat pemain menekan tombol (mouse atau tombol ESP32), hook akan menembak ke bawah.

Jika hook mencapai batas bawah atau mengenai objek, ia akan kembali ke posisi awal.

Timer game berjalan (misalnya 60 detik), dan saat habis, scene akan restart.

Saat scene restart, koneksi serial dengan ESP32 tetap aman karena port ditutup otomatis.

🛠️ Implementasi di Unity
1. Kontrol Hook (Mouse)
Hook diatur dalam script HookMovement.cs untuk rotasi otomatis (Rotate()).

Saat Input.GetMouseButtonDown(0), hook turun ke bawah.

RopeRenderer menggambar tali hook.

Potongan kode untuk mouse:

csharp
void GetInput()
{
    bool mousePressed = Input.GetMouseButtonDown(0);

    if (mousePressed && canRotate)
    {
        canRotate = false;
        moveDown = true;
    }
}
2. Integrasi ESP32 (Serial Port)
Menambahkan pembacaan serial di Unity dengan System.IO.Ports.

Jika menerima string "FIRE" dari ESP32, jalankan logika yang sama dengan klik mouse.

Port serial ditutup di OnDestroy() agar saat scene restart tidak terjadi Access Denied.

Potongan kode:

csharp
void GetInput()
{
    bool mousePressed = Input.GetMouseButtonDown(0);
    bool espPressed = (serialInput == "FIRE");

    if ((mousePressed || espPressed) && canRotate)
    {
        canRotate = false;
        moveDown = true;
        serialInput = ""; // reset setelah digunakan
    }
}

void OnDestroy()
{
    if (serialPort != null && serialPort.IsOpen)
    {
        serialPort.Close();
        System.Threading.Thread.Sleep(200); // beri waktu OS melepas port
    }
}
💻 Kode ESP32
ESP32 digunakan untuk membaca tombol fisik, lalu mengirim "FIRE" ke Unity jika ditekan.
Kode sudah menggunakan sistem debounce agar sekali tekan hanya mengirim satu perintah.

cpp
#include <Arduino.h>

#define buttonPin 26

bool lastButtonState = HIGH;
unsigned long lastDebounceTime = 0;
const unsigned long debounceDelay = 50; // ms

void setup() {
  Serial.begin(115200);
  pinMode(buttonPin, INPUT_PULLUP);
}

void loop() {
  bool reading = digitalRead(buttonPin);

  if (reading != lastButtonState) {
    lastDebounceTime = millis();
  }

  if ((millis() - lastDebounceTime) > debounceDelay) {
    if (lastButtonState == HIGH && reading == LOW) {
      Serial.println("FIRE");
    }
  }

  lastButtonState = reading;
}
🔌 Wiring
ESP32 pin 26 → Push Button → GND
Push Button → 3.3V (melalui internal pull-up)
🚀 Cara Menjalankan
Buat game Gold Miner di Unity dengan script HookMovement untuk kontrol hook.

Pastikan timer dan restart scene sudah berjalan (misalnya dengan SceneManager.LoadScene).

Upload kode ESP32 ke board via Arduino IDE.

Sambungkan ESP32 ke PC.

Di Unity, sesuaikan nama port di:

csharp
Copy
Edit
SerialPort serialPort = new SerialPort("COM5", 115200);
Jalankan game di Unity:

Klik mouse → hook turun.

Tekan tombol ESP32 → hook turun.

