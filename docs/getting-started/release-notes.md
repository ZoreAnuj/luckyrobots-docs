---
icon: material/note-text-outline
---

# Release Notes

Patch history for this release line. Each heading is a minor patch and is directly
linkable, e.g. [2026.1](#2026.1). For the feature highlights of a release, see
[What's New](whats-new.md).

<!--
  Newest patch first — MkDocs renders headings top-down with no auto-sort.

  Explicit `{ #2026.1.1 }` anchors are required: the default slugify strips dots, so a
  plain `## 2026.1.1` would resolve to `#202611`. The explicit id keeps the anchor exactly
  `2026.1.1`, so /2026.1/release-notes/#2026.1.1 works.

  This file is per release line: on the release/2026.1 branch it lists 2026.1.x; on
  release/2026.2 it lists 2026.2.x. mike serves each under its own /<version>/ prefix.
-->

## 2026.1.1 { #2026.1.1 }
_19 June 2026_

Minor update with fixes and improvements.

- Added MuJoCo Import Editor (double-click on MuJoCo XML in Content Browser)
- Added better .NET version detection and user feedback
- Added more macOS ffmpeg search paths, eg. homebrew
- Added API Reset Settle Steps to Scene Settings
- Changed API function/enum names and docs to encourage AI agents to reason better
- Improved MuJoCo XML support and import stability
- Fixed vsync using incorrect presentation mode resulting in higher than necessary GPU usage
- Fixed Agent panel always being open on startup
- macOS crash fixes:
	- Fixed startup crash due to conflicting Vulkan/MoltenVK loaders
	- Fixed crash when resizing window

## 2026.1 { #2026.1 }
_12 June 2026_

The first public release. See [What's New](whats-new.md) for the full feature list.
