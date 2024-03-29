# ZbW.DbAdv.Projektarbeit

new diagram (drawio)
* [Diagrams](https://app.diagrams.net/#G1GT2RS8TQN9hldJ3JVkAbapVrslOVwISu)
* !! Wichtig !! Man muss glaube ich manuell speichern?

Arbeitsjournal
* [Arbeitsjournal (Google Tabelle)](https://docs.google.com/spreadsheets/d/1qFB4jnwHBaJTzRITUQM5Yf46yaIUyNb5hPTvtonPae4/edit#gid=0)

old lucidchart references
DbAdv Projektarbeit

* [Architektur](https://lucid.app/lucidchart/5cb971f2-94ad-4085-9b2c-d86661d604c4/edit?beaconFlowId=79310CD528FE96A6&invitationId=inv_894731d8-edc7-44aa-b806-c87a313f5ca6&page=qrCw5ASc-_YO#)
* [Klassendiagramm](https://lucid.app/lucidchart/5cb971f2-94ad-4085-9b2c-d86661d604c4/edit?beaconFlowId=79310CD528FE96A6&invitationId=inv_894731d8-edc7-44aa-b806-c87a313f5ca6&page=WvCwLZmK0hVw#)
* [ERM](https://lucid.app/lucidchart/5cb971f2-94ad-4085-9b2c-d86661d604c4/edit?beaconFlowId=79310CD528FE96A6&invitationId=inv_894731d8-edc7-44aa-b806-c87a313f5ca6&page=LvCwf.b9mbn6#)

## Lieferergebnisse
* Dokumentation (PDF)
* Arbeitsjournal (PDF)



## Thema RegEx

### KundenNr ✔️
Die Adressnummer muss zwingend mit dem Präfix «CU» beginnen (CU=Customer). Anschliessend soll eine 5stellige Nummer folgen.
* Regex Rule: https://regex101.com/r/pQDccg/1 

### E-Mail-Adresse ✔️
Implementieren Sie eine Validierung für Email-Adressen. Wenn Ihre Validierung nicht alle
Fälle gemäss RFC 5322 abdeckt (weil z.B. das RegEx-Pattern für die letzten 1% Spezialfälle
zu kompliziert würde) dokumentieren Sie, welche Fälle nicht abgedeckt sind.
* [RFC 5322](https://www.rfc-editor.org/rfc/rfc5322)
* https://regex101.com/r/XEnnnF/5


### Webseite ✔️
Die Validierung für die Eingabe der Website soll folgende Formate zulassen:
* www.google.com
* http://www.google.com
* https://www.google.com
* google.com

Wahlweise soll eine Subdomain angegeben werden können (gilt für alle vier Formate) – bspw.: https://policies.google.com
Die Adresse soll auch beliebig mit einem Pfad sowie Parameter ergänzt werden können(dies auch wieder für alle Formate) – bspw.: https://policies.google.com/technologies/voice?hl=de&gl=ch

* Regex Rule: https://regex101.com/r/dHfnxP/2

### Passwort ✔️
Für Passwörter gelten die folgenden Regeln:
* Min. 8 Zeichen (bis 12 Zeichen)
* Zwingend einen Gross- sowie einen Kleinbuchstaben
* Zwingend eine Zahl
* usw. + Sonderzeichen

Regex Rule:
* https://regex101.com/r/7eDnhK/2

## Thema Serialization

Kundendaten sollen importiert und bezogen auf einen bestimmten Zeitpunkt (temporal)
exportiert werden können. Dafür soll im UI für jede dieser Funktionen eine Schaltfläche
zur Verfügung stehen. Wird der Export gestartet, kann der Benutzer Datum/Zeit wählen.
Diese Eingabe wird dann verwendet um die Daten entsprechend zu ermitteln.
Die Daten sollen im JSON- und XML-Format importiert und exportiert werden können. Der
Aufbau der entsprechenden Dateien ist nachfolgend aufgezeigt:

JSON:
```json
[{
    "customerNr": "CU1234",
    "name": "Hans Muster",
    "address": {
        "street": "Musterweg 1",
        "postalCode": 1234
    },
    "email": "hans@muster.ch",
    "website": "www.hansmuster.ch",
    "password": "hiddensecret"
}]
```
XML:
```xml
<Kunden>
    <Kunde CustomerNr="CU1234">
        <Name>Hans Muster</Name>
            <Address>
                <Street>Musterweg 1</Street>
                <PostalCode>1234</PostalCode>
            </Address>
        <EMail>hans@muster.ch</EMail>
        <Website>www.hansmuster.ch</Website>
        <Password>hiddensecret</Password>
    </Kunde>
</Kunden>
```
Überlegen Sie sich im Zusammenhang mit dieser Anforderung ebenfalls, wie Sie ein
Passwort sicher exportieren können. D.h. wenn jemand die exportierte Datei öffnet, darf
es unter keinen Umständen möglich sein, das Passwort im Klartext auslesen zu können.