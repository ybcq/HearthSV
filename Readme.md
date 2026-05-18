# HearthSV - 炉石大战影之诗

<div align="center">

![HearthSV](https://img.shields.io/badge/HearthSV-炉石大战影之诗-blue?style=for-the-badge&logo=github)
![Version](https://img.shields.io/badge/Version-1.0.0-green?style=for-the-badge)
![Status](https://img.shields.io/badge/Status-开发中-yellow?style=for-the-badge)
![License](https://img.shields.io/badge/License-MIT-red?style=for-the-badge)

**一个基于HSMOD的炉石战棋DIY MOD项目**

[项目介绍](#项目介绍) • [功能特性](#功能特性) • [安装指南](#安装指南) • [使用说明](#使用说明) • [开发文档](#开发文档) • [更新日志](#更新日志) • [贡献指南](#贡献指南)

</div>

---

## 📋 目录

- [项目介绍](#项目介绍)
- [功能特性](#功能特性)
- [安装指南](#安装指南)
- [使用说明](#使用说明)
- [项目结构](#项目结构)
- [开发文档](#开发文档)
- [更新日志](#更新日志)
- [贡献指南](#贡献指南)
- [许可证](#许可证)
- [联系方式](#联系方式)

---

## 🎮 项目介绍

HearthSV是一个基于HSMOD开发的炉石战棋DIY MOD项目，旨在创造一个融合炉石传说与影之诗元素的创新卡牌游戏体验。项目虽然因时间原因最终放弃，但包含了完整的开发资源、修改教程和历代版本更新日志，为后续开发者提供了宝贵的参考基础。

### 🎯 项目目标

- 创建一个融合炉石传说与影之诗元素的卡牌游戏
- 实现酒馆战棋模式的DIY版本
- 提供完整的卡牌修改和资源管理工具
- 建立开源社区，促进后续开发

---

## ✨ 功能特性

### 🃏 核心功能
- **酒馆战棋模式** - 完整的酒馆战棋游戏机制
- **影之诗元素** - 融入影之诗卡牌和技能系统
- **卡牌编辑器** - 支持卡牌属性、技能、美术资源的修改
- **资源管理** - 完整的游戏资源提取和管理工具

### 🛠️ 开发工具
- **AssetStudio** - 游戏资源提取工具
- **AssetBundleExtractor** - 资源包解压工具
- **dnSpy** - 代码反编译和修改工具
- **HSMOD动作模板** - 快速开发工具

### 🎨 视觉资源
- **卡牌美术** - 大量原创和修改的卡牌图片
- **界面设计** - 完整的游戏UI资源
- **特效资源** - 各种游戏特效和动画

---

## 📦 安装指南

### 系统要求
- Windows 10/11
- .NET Framework 4.5+
- 至少4GB可用磁盘空间

### 安装步骤

1. **下载项目**
   ```bash
   git clone https://github.com/yourusername/HearthSV.git
   cd HearthSV
   ```

2. **安装依赖工具**
   - 解压 `修改工具/` 目录下的工具包
   - 确保所有工具都在同一目录下

3. **运行游戏**
   - 双击 `HearthSV.exe` 启动游戏
   - 或使用命令行运行：
   ```bash
   HearthSV.exe
   ```

### 工具安装

#### 必需工具
- **AssetStudio** - 用于提取游戏资源
- **AssetBundleExtractor** - 用于解压资源包
- **dnSpy** - 用于修改代码

#### 可选工具
- **HSMOD动作模板** - 快速开发工具
- **Photoshop** - 卡牌图片编辑（需要提供的动作模板）

---

## 🎯 使用说明

### 基本操作

1. **启动游戏**
   - 双击 `HearthSV.exe` 启动主程序
   - 选择游戏模式开始游戏

2. **卡牌编辑**
   - 使用 `修改工具/` 中的工具进行卡牌修改
   - 参考 `修改工具/内测须知.md` 了解详细操作

3. **资源管理**
   - 使用 AssetStudio 提取游戏资源
   - 使用 AssetBundleExtractor 处理资源包

### 开发流程

1. **环境准备**
   ```bash
   # 确保所有工具都已正确安装
   # 检查工具版本兼容性
   ```

2. **资源提取**
   ```bash
   # 使用 AssetStudio 提取原版资源
   # 备份原始资源文件
   ```

3. **修改开发**
   ```bash
   # 使用 dnSpy 修改代码逻辑
   # 使用 Photoshop 修改卡牌图片
   # 测试修改效果
   ```

4. **打包发布**
   ```bash
   # 重新打包资源文件
   # 测试完整游戏流程
   # 发布新版本
   ```

---

## 📁 项目结构

```
HearthSV/
├── Readme.md                 # 项目说明文档
├── .gitignore                # Git忽略文件
├── HearthSV.exe              # 主程序
├── HSMOD基础代码简介.chm      # 开发文档
├── 官方原版/                  # 原版游戏文件
├── 修改工程/                  # 修改后的游戏文件
│   ├── 酒馆战棋/              # 酒馆战棋相关资源
│   ├── 酒馆战棋-改/           # 修改版酒馆战棋
│   ├── 酒馆战棋-圆/           # 圆形版酒馆战棋
│   ├── 影之诗/               # 影之诗相关资源
│   ├── 影之诗-圆/            # 圆形版影之诗
│   ├── 难度卡/               # 难度调整卡牌
│   └── 其他资源/              # 其他游戏资源
├── 修改工具/                  # 开发工具
│   ├── AssetStudio.exe       # 资源提取工具
│   ├── AssetBundleExtractor.exe # 资源包解压工具
│   ├── dnSpy.exe             # 代码修改工具
│   ├── HSMOD动作模板.exe      # 快速开发工具
│   └── Photoshop动作/         # 图片编辑模板
├── 演示图片/                  # 游戏演示图片
└── HearthSV_Data/            # 游戏数据文件
```

---

## 📚 开发文档

### 核心概念

#### HSMOD架构
- 基于Unity引擎的游戏框架
- 支持模块化开发
- 提供完整的卡牌系统API

#### 卡牌系统
- 卡牌属性定义
- 技能系统实现
- 美术资源管理

#### 游戏模式
- 酒馆战棋模式
- 影之诗模式
- 自定义模式

### 代码结构

主要模块：
- **卡牌管理** - 卡牌的创建、编辑、删除
- **战斗系统** - 回合制战斗逻辑
- **AI系统** - 电脑对手AI
- **资源管理** - 游戏资源的加载和管理

---

## 📝 更新日志

### v1.0.0 (2020-08-20)
- ✅ 初始版本发布
- ✅ 完成基础酒馆战棋模式
- ✅ 实现影之诗元素融合
- ✅ 提供完整的开发工具集

### v0.9.0 (2020-05-01)
- 🔧 优化卡牌平衡性
- 🔧 改进AI系统
- 🔧 修复已知bug
- 🔧 添加新的卡牌资源

### v0.8.0 (2020-03-15)
- 🎨 更新卡牌美术资源
- 🎨 优化游戏界面
- 🎨 添加新的特效
- 🎨 改进用户体验

---

## 🤝 贡献指南

我们欢迎所有形式的贡献！请遵循以下步骤：

### 1. Fork 项目
```bash
git clone https://github.com/yourusername/HearthSV.git
cd HearthSV
```

### 2. 创建分支
```bash
git checkout -b feature/your-feature-name
```

### 3. 提交更改
```bash
git add .
git commit -m "Add: your feature description"
```

### 4. 推送到分支
```bash
git push origin feature/your-feature-name
```

### 5. 创建 Pull Request
- 详细描述你的更改
- 确保代码风格一致
- 提供必要的测试

### 开发规范

- **代码风格** - 遵循C#编码规范
- **提交信息** - 使用清晰的提交信息格式
- **文档更新** - 更新相关文档
- **测试** - 确保功能正常工作

---

## 📄 许可证

本项目采用 MIT 许可证 - 查看 [LICENSE](LICENSE) 文件了解详情。

---

## 📞 联系方式

- **项目主页**: [GitHub Repository](https://github.com/yourusername/HearthSV)
- **问题反馈**: [Issues](https://github.com/yourusername/HearthSV/issues)
- **开发文档**: [项目文档](https://ybcq.github.io/2020/08/20/%E3%80%90%E6%96%B0MOD%E3%80%91HearthSV%E5%86%85%E6%B5%8B%E7%BB%84%E6%8B%9B%E5%8B%9F/)
- **更新日志**: [更新日志](https://ybcq.github.io/2020/05/01/%E3%80%90%E6%96%B0MOD%E3%80%91HearthSV%E6%9B%B4%E6%96%B0%E6%97%A5%E5%BF%97/)

---

<div align="center">

**⭐ 如果这个项目对你有帮助，请给我们一个Star！**

![Star History](https://img.shields.io/github/stars/yourusername/HearthSV?style=social)

</div>

