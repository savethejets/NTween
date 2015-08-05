# NTWeen

NTween is a facade layer between your Unity3D code 
and your tween engine.

Using NTween you can easily change out your tween 
engine if you need to and not have to alter all your code.

## HOW TO USE

import the code into your unity3D project.

Depending on which tween engine you’re using you’ll need to add
a preprocessor directive for that engine.

Select from the menu Edit > Project Settings > Player 

Find the Box for “Scripting Define Symbols”

Enter HOTWEEN_TYPE if you’re using HOTWEEN
Enter DOTWEEN_TYPE if you’re using DOTWEEN.
(more to come…)

## NTween helper methods

NTween has some extension methods that will help you create clean code

ex.   transform.TweenPosition(new Vector3(1,1,1), 0.5f);
(this call transforms the position to 1,1,1 in 0.5f seconds) 

Check them out in the class NTweenHelper there’s a couple of helper methods in there.

If the thing you’re tweening isn’t a Transform or a SpriteRenderer you can manually create 
the tween

var builder = new NTweenBuilder (_sprite, 0.1f, "dimensions", OpenDimension);

NTween.Instance.CreateTween (builder); 

You can also attach Attributes to the tweens such as Delay, etc with the NTweenAttribute class.


The MIT License (MIT)

Copyright (c) 2015 Kyle Reczek

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
 


