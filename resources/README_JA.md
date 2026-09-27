<img src="secrandom-icon-paper.png" width="128" height="128" alt="UNRandom" />

# UNRandom

**シンプルで高速なランダム抽選ツール**

[![License: GPL v3](https://img.shields.io/badge/License-GPLv3-blue.svg?style=for-the-badge)](../LICENSE)

> [!IMPORTANT]
> 本リポジトリは [SecRandom](https://github.com/SECTL/SecRandom) の派生版です。
> オリジナルは SECTL が開発し、作者は 黎泽懿_Aionflux です。ライセンスは GNU GPLv3 です。
> 本ビルドではテレメトリ、抽選会、更新チェック、抽選検証を削除し、点呼とクイック抽選のみを残し、抽選設定を1ページに統合しています。

## 特徴

- **1ページの設定**：点呼とクイック抽選は1つの抽選設定を共有し、上書きスイッチやグループ分けはありません
- **クイック抽選フローティングウィンドウ**：独立ウィンドウでワンクリック抽選、クリック後の無効クールダウンも設定可能
- **公平な重み付け**：履歴回数、抽選間隔、グループ、性別に基づき重みを動的に調整し、重複と偏りを軽減
- **名簿管理**：複数名簿、`.xlsx`/`.xls`/`.csv` のインポート、マッピング、プレビューに対応
- **セキュリティ**：パスワード、TOTP、USB メモリで重要な操作を保護
- **データ送信なし**：いかなるデータも送信せず、更新チェックもありません

## ダウンロード

- [GitHub Releases](https://github.com/mathfosh/UNRandom/releases) で本バージョンのパッケージを提供
- [上游 SecRandom Releases](https://github.com/SECTL/SecRandom/releases) でオリジナルの各バージョンを提供

## ライセンスとサードパーティ通知

- [GNU GPLv3](../LICENSE) で公開されており、派生物も GPLv3 に従う必要があります
- サードパーティコンポーネントと著作権情報は [THIRD-PARTY-NOTICES.md](../THIRD-PARTY-NOTICES.md) を参照してください
- 履歴バランスによる重み付けと候補フィルタは重複抽選の軽減と長期的な分布の改善を支援しますが、現実の名簿やルール、プロセスの管理に代わるものではなく、それらをソフトウェアが検証できるとは主張しません
