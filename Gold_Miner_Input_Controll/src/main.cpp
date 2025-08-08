#include <Arduino.h>

#define buttonPin  26
#define LED_PIN 2

bool lastButtonState = HIGH;  // Previous button state
bool currentButtonState = HIGH;  // Current button state

void setup() {
  Serial.begin(115200);
  pinMode(buttonPin, INPUT_PULLUP);
  pinMode(LED_PIN, OUTPUT);
  digitalWrite(LED_PIN, LOW); // Ensure LED is off initially
}

void loop() {
  // Read the current button state
  currentButtonState = digitalRead(buttonPin);

  // Check if button was just pressed (transition from HIGH to LOW)
  if (lastButtonState == HIGH && currentButtonState == LOW) {
    Serial.println("FIRE");
    digitalWrite(LED_PIN, HIGH); // Turn on LED
    delay(100); // Keep LED on for a short duration
    digitalWrite(LED_PIN, LOW); // Turn off LED
  }

  // Update the last button state
  lastButtonState = currentButtonState;
  
  delay(20); // Delay for readability
}