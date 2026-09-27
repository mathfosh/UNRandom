<img src="secrandom-icon-paper.png" width="128" height="128" alt="UNRandom" />

# UNRandom

**A simple, fast random-selection tool**

[![License: GPL v3](https://img.shields.io/badge/License-GPLv3-blue.svg?style=for-the-badge)](../LICENSE)

> [!IMPORTANT]
> This repository is a derivative of [SecRandom](https://github.com/SECTL/SecRandom).
> The original project is developed by SECTL, authored by 黎泽懿_Aionflux, and licensed under GNU GPLv3.
> This build removes telemetry, lottery, update checks, and draw verification, keeping only roll-call and quick draw, and merges the draw settings into a single page.

## Highlights

- **One page of settings**: roll-call and quick draw share a single draw config, with no override toggles or grouping
- **Quick-draw floating window**: a standalone window for one-click draws, with an optional cooldown after each click
- **Fair weights**: dynamically adjusts weights by history count, draw interval, group, and gender to reduce repeats and distribution skew
- **List management**: multiple student lists, `.xlsx`/`.xls`/`.csv` import, mapping, and preview
- **Security**: protect important operations with a password, TOTP, or USB drive
- **No phone-home**: no data is ever reported, and there is no update check

## Download

- [GitHub Releases](https://github.com/mathfosh/UNRandom/releases) provides packages for this fork
- [Upstream SecRandom Releases](https://github.com/SECTL/SecRandom/releases) provides the original project releases

## License and third-party notices

- Released under [GNU GPLv3](../LICENSE); derivative works must also use GPLv3
- See [THIRD-PARTY-NOTICES.md](../THIRD-PARTY-NOTICES.md) for third-party components and copyright information
- History-balanced weights and candidate filters help reduce repeat selections and improve long-term distribution. They do not replace management of real-world rosters, rules, or processes, and SecRandom does not claim to verify those conditions.
