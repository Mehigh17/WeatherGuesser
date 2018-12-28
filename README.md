# WeatherGuesser

## Description

WeatherGuesser is a small piece of software capable of learning then distinguishing different types of weathers by looking at the clouds.
The software isn't technically restrained to clouds only. You can feed it images of animals or anything else and it will just label it wrong when you evaluate the image.

The classification model used is a Multiclass Support Vector Machine, it is easily implemented thanks to the [Accord.NET](http://accord-framework.net/) framework which offers an enormous variety of models and machine learning techniques.

## How to train

Since it's restrained to only 3 types of weathers (Sunny, Cloudy, Thunderstorm), all you must do is create a folder hierarchy like this:

```
|- root_folder
 |--- sunny
   |- picture_of_sunny_sky_1.jpg 
   |- picture_of_sunny_sky_2.jpg
   |- etc.
 |--- cloudy
   |- picture_of_sunny_sky_1.jpg
   |- picture_of_sunny_sky_2.jpg
   |- etc.
 |--- thunderstorm
   |- picture_of_sunny_sky_1.jpg
   |- picture_of_sunny_sky_2.jpg
   |- etc.
```

Once you've got your hierarchy right, click `Load Training Data` inside the app and select your *root_folder*, then do `Start Training`.

## How is image normalization handled

1. The image is first cropped at 1:1 ratio down to its lowest dimension. *(ex: if the height is smaller than the width then the square will have its sides length equal to the height)*
2. The cropped image is then rescaled to 128x128 to ensure faster training at the big cost of precision in the machine's decision. *(remember, this is just a toy)*
3. The rescaled image is then transformed into an array of grayscale points.

Only the array of grayscale points can be fed to the learning service. You can use the `StandardNormalizeService` in order to achieve what has just been described.

## Example

![Example](https://i.imgur.com/lUg7zD5.png)