<img src="resources/secrandom-icon-paper.png" width="128" height="128" alt="UNRandom" />

# UNRandom

**简洁、快速的随机抽取工具**

[![License: GPL v3](https://img.shields.io/badge/License-GPLv3-blue.svg?style=for-the-badge)](LICENSE)

> [!IMPORTANT]
> 本仓库是 [SecRandom](https://github.com/SECTL/SecRandom) 的衍生版本。
> 原项目由 SECTL 思拓创联开发，原作者 黎泽懿_Aionflux，遵循 GNU GPLv3。
> 本版本移除了遥测上报、抽奖、更新检查与抽取验证，只保留点名与闪抽，并合并了抽取设置页。

## 特点

- **一页设置**：点名与闪抽共用一份抽取配置，不再有覆盖开关与分组
- **闪抽悬浮窗**：独立窗口一键抽取，点击后可设禁用冷却
- **公平权重**：按历史次数、抽取间隔、分组、性别动态调整权重，降低重复与分布失衡
- **名单管理**：支持多名单、`.xlsx`/`.xls`/`.csv` 导入映射与预览
- **安全保护**：可用密码、TOTP 或 U 盘保护重要操作
- **无后门**：不联网上报任何数据，不含更新检查

## 下载

- [GitHub Releases](https://github.com/mathfosh/UNRandom/releases) 提供本版本的发行包
- [上游 SecRandom Releases](https://github.com/SECTL/SecRandom/releases) 提供原项目各版本

## 许可证与第三方声明

- 基于 [GNU GPLv3](LICENSE) 协议发布，衍生作品须同样遵循 GPLv3
- 第三方组件与版权信息见 [THIRD-PARTY-NOTICES.md](THIRD-PARTY-NOTICES.md)
- 历史平衡的权重与候选过滤策略帮助降低重复抽取、改善长期分布，但不替代对现实名单、规则或流程的管理，也不对这些现实条件作出软件无法验证的保证
