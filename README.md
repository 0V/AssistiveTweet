AssistiveTweet
==============

進捗ツイート用クライアント

## 概要
- 進捗ツイート用クライアントです。
- 個人的に使うために作りました。
  
  
## UI がアレっぽくない？
- UI が iOS の [アレ](http://support.apple.com/kb/HT5587?viewlocale=ja_JP) っぽいのは仕様です。
- 名前も iOS の [アレ](http://support.apple.com/kb/HT5587?viewlocale=ja_JP) っぽいのはもちろん仕様です。
- この UI 部分の XAML を引っこ抜いて適当に改造すればわりと使えるんじゃないでしょうか。
  
  
## スペック
- 「進捗を第一に考える。あと自分が使いやすければそれで良い」が基本方針です。
- とても軽く進捗の邪魔になりません。(TL ウィンドウのリサイズが重い点(うまく仮想化できていないため)を除けば)
- 進捗の邪魔にならないよう、ツイートとTL監視しかできません。
- もちろんふぁぼれません。
- ふぁぼは進捗の敵だという宗教観がもとになっています。


## ビルド前に
- ソースコードのビルドを行う前に App.xaml.cs 内の ConsumerKey と ConsumerSecret にあなたのクライアントのキーを登録してください。
- [リリースされているもの](https://github.com/0V/AssistiveTweet/releases)については、AssistiveTweet のキーが登録してあります。
