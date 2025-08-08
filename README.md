Oke, ini aku buatin langsung format **README.md** yang sudah rapi, jadi tinggal kamu copy-paste ke file `README.md` di proyek Unity kamu.

````markdown
# 🎣 Gold Miner – Hook Movement with ESP32 Button Control

## 📌 Deskripsi
Proyek ini adalah implementasi **game Gold Miner** di Unity dengan sistem kontrol hook (pengait) yang dapat bergerak otomatis bolak-balik, lalu menembak ke bawah saat pemain menekan tombol.  
Awalnya kontrol dilakukan dengan klik mouse, kemudian diintegrasikan dengan **ESP32** dan push button untuk memberikan pengalaman bermain yang lebih interaktif dengan kontrol fisik.

---

## 🎮 Mekanisme Gameplay
1. Hook bergerak otomatis ke kiri-kanan dalam batas sudut tertentu.
2. Saat pemain menekan tombol (mouse atau tombol ESP32), hook akan menembak ke bawah.
3. Jika hook mencapai batas bawah atau mengenai objek, ia akan kembali ke posisi awal.
4. Timer game berjalan (misalnya 60 detik), dan saat habis, scene akan restart.
5. Saat scene restart, koneksi serial dengan ESP32 tetap aman karena port ditutup otomatis.

---

## 🛠️ Implementasi di Unity

### 1. Kontrol Hook (Mouse)
Hook diatur dalam script `HookMovement.cs` untuk rotasi otomatis (`Rotate()`).  
Saat `Input.GetMouseButtonDown(0)`, hook turun ke bawah.  
`RopeRenderer` digunakan untuk menggambar tali hook.

```csharp
void GetInput()
{
    bool mousePressed = Input.GetMouseButtonDown(0);

    if (mousePressed && canRotate)
    {
        canRotate = false;
        moveDown = true;
    }
}
````

---

### 2. Integrasi ESP32 (Serial Port)

Menambahkan pembacaan serial di Unity dengan `System.IO.Ports`.
Jika menerima string `"FIRE"` dari ESP32, jalankan logika yang sama dengan klik mouse.
Port serial ditutup di `OnDestroy()` agar saat scene restart tidak terjadi **Access Denied**.

```csharp
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
```

---

## 💻 Kode ESP32

ESP32 digunakan untuk membaca tombol fisik, lalu mengirim `"FIRE"` ke Unity jika ditekan.
Kode sudah menggunakan sistem **debounce** agar sekali tekan hanya mengirim satu perintah.

```cpp
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
```

---

## 🔌 Wiring

```
ESP32 pin 26 → Push Button → GND
Push Button → 3.3V (melalui internal pull-up)
```

---

## 🚀 Cara Menjalankan

1. **Buat game Gold Miner** di Unity dengan script `HookMovement` untuk kontrol hook.
2. Pastikan timer dan restart scene sudah berjalan (misalnya dengan `SceneManager.LoadScene`).
3. Upload kode ESP32 ke board via Arduino IDE.
4. Sambungkan ESP32 ke PC.
5. Di Unity, sesuaikan nama port di:

   ```csharp
   SerialPort serialPort = new SerialPort("COM5", 115200);
   ```
6. Jalankan game di Unity:

   * Klik mouse → hook turun.
   * Tekan tombol ESP32 → hook turun.

---

## 📝 Catatan

* Jangan buka Arduino Serial Monitor bersamaan dengan Unity, karena port akan terkunci.
* Delay 200 ms di `OnDestroy()` membantu Windows melepas port sebelum dibuka lagi.
* Bisa diperluas dengan dua tombol (misalnya untuk kontrol ekstra di game).

```

Kalau mau, README ini bisa aku tambahkan **diagram alur gameplay** Gold Miner + **flow data** ESP32 → Unity → Hook supaya lebih visual.  
Mau aku tambahkan sekalian diagramnya?
```
