<img src="resources/secrandom-icon-paper.png" width="128" height="128" alt="UNRandom" />

# UNRandom

**基于动态权重的公平随机工具，让抽取与决策告别争议**

[![License: GPL v3](https://img.shields.io/badge/License-GPLv3-blue.svg?style=for-the-badge)](LICENSE)

> [!IMPORTANT]
> 本仓库是 [SecRandom](https://github.com/SECTL/SecRandom) 的衍生版本。
> 原项目由 SECTL 思拓创联开发，原作者 黎泽懿_Aionflux，遵循 GNU GPLv3。
> 本版本移除了遥测上报、抽奖与更新功能，并调整了抽取设置页。

> [!NOTE]
> SecRandom 以 GNU GPLv3 协议发布！您可以修改和再发布源代码，但再发布的衍生作品也必须遵循 GNU GPLv3

## UNRandom

UNRandom 是面向课堂、团队、活动、决策等场景的公平抽取应用

## 软件功能

### 抽取流程

- **点名**：支持普通随机、历史平衡及重复控制
- **闪抽**：通过独立悬浮窗快速抽取学生
- **丰富呈现**：统一配置动画、结果、语音、音乐和通知，并支持通知失败回退

### 公平与名单管理

- 根据历史次数、抽取间隔、分组、性别等因素动态调整权重，降低重复与分布失衡
- 使用稳定内部标识维护历史，学号、编号和名称仅作为显示信息
- 支持多名单、多奖品池及 `.xlsx`、`.xls`、`.csv` 导入、映射和预览
- 每轮抽取均保存历史，方便查询和回顾

### 数据、隐私与安全

- 设置、名单和历史记录都可以导入、导出、备份和恢复
- 备份可以包含名单、历史、图片、音频等信息，但不会包含密码等安全信息
- 支持使用密码、TOTP或 U 盘保护重要操作，并可设置哪些操作需要验证


## 技术演进

| 版本 | 技术栈 | 阶段 |
| --- | --- | --- |
| v1 | Python + PyQt5 + qfluentwidgets | 初代桌面实现 |
| v2 | Python + PySide6 + qfluentwidgets | Qt 技术栈演进 |
| **v3** | **C# + Avalonia + FluentAvalonia** | .NET 桌面重构，持续发展抽取与桌面集成能力 |

## 下载与更新

- [GitHub Releases](https://github.com/SECTL/SecRandom/releases) 提供上游各版本的发行包与说明
- 本版本不包含更新检查功能，升级需手动替换安装包

## 许可证与第三方声明

- SecRandom 使用 [GNU GPLv3](LICENSE) 协议发布
- 第三方组件、版权和分发审查信息见 [THIRD-PARTY-NOTICES.md](THIRD-PARTY-NOTICES.md)
- SecRandom 通过历史平衡的权重与候选过滤策略帮助降低重复抽取、改善长期分布；它不替代对现实名单、规则或组织流程的管理，也不对这些现实条件作出软件无法验证的保证

## 贡献者和特别感谢

<a href="https://github.com/SECTL/SecRandom/graphs/contributors">
  <img src="https://contrib.rocks/image?repo=SECTL/SecRandom" alt="SecRandom contributors" />
</a>

感谢每一位为 SecRandom 提交代码、报告问题、完善文档和提供反馈的贡献者。头像由 GitHub 贡献者数据动态生成，点击可前往 [GitHub 贡献者页面](https://github.com/SECTL/SecRandom/graphs/contributors) 查看完整统计

## 支持与社区

- [爱发电支持](https://afdian.com/a/lzy0983)
- [邮箱](mailto:lzy.12@foxmail.com)
- [QQ群 833875216](https://qm.qq.com/q/iWcfaPHn7W)
- [QQ 频道](https://pd.qq.com/s/4x5dafd34?b=9)
- [Bilibili 主页](https://space.bilibili.com/520571577)
- [问题反馈](https://github.com/SECTL/SecRandom/issues)
- [SecRandom 官方文档](https://secrandom.sectl.cn/doc/overview.html)
- [![Ask DeepWiki](https://deepwiki.com/badge.svg)](https://deepwiki.com/SECTL/SecRandom)
- [简体中文贡献指南](CONTRIBUTING.md)



