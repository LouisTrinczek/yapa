## Table of Contents

1. [Overview](#overview)
2. [Entities](#entities)
    3. [`user`](#user)
    4. [`poll`](#poll)
    5. [`poll_option`](#poll_option)
    6. [`vote`](#vote)
    7. [`poll_invite`](#poll_invite)
    8. [`result_snapshot`](#result_snapshot)
9. [Enumerations](#enumerations)
10. [Relationships](#relationships)
11. [Constraints & Indexes (Conceptual)](#constraints--indexes-conceptual)
12. [Future Extensions](#future-extensions)

---

## Overview

This document **lists the logical entities and their attributes** for a polling platform that supports:

- Single- or multiple-choice voting
- Optional deadlines
- Shareable links
- Vote-integrity mechanisms (IP, cookie, session, account level)

---

## Entities
### `BaseEntity`
The BaseEntity contains Data all entities contain

| Field        | Type      | Constraints     | Description            |
| ------------ | --------- | --------------- | ---------------------- |
| `id`         | UUID      | **PK**          | Identifier             |
| `created_at` | TIMESTAMP | default `now()` | Row creation timestamp |
| `updated_at` | TIMESTAMP | auto-updating   | Last modification      |
| archived_at  | TIMESTAMP | NULL            | Time of archival       |

### `user`

| Field           | Type         | Constraints          | Description                    |
| --------------- | ------------ | -------------------- | ------------------------------ |
| `username`      | VARCHAR(50)  | **UNIQUE**, NOT NULL | Display / login name           |
| `email`         | VARCHAR(254) | **UNIQUE**, NOT NULL | Used for login & notifications |
| `password_hash` | TEXT         | NOT NULL             | Secure hash (BCrypt)           |
| salt            | TEXT         | NOT NULL             | Salt for hashing               |


---

### `poll`

| Field                   | Type              | Constraints             | Description             |
| ----------------------- | ----------------- | ----------------------- | ----------------------- |
| `creator_id`            | UUID              | **FK** → `user.user_id` | Poll owner              |
| `title`                 | VARCHAR(140)      | NOT NULL                | Poll question           |
| `description`           | TEXT              | NULL                    | Long-form context       |
| `share_slug`            | VARCHAR(64)       | **UNIQUE**              | Friendly URL segment    |
| `allow_multiple_choice` | BOOLEAN           | DEFAULT `FALSE`         | Single vs multiple vote |
| `deadline`              | TIMESTAMP         | NULL                    | Closing time            |
| `vote_protection`       | `vote_protection` | DEFAULT `'none'`        | Anti-fraud strategy     |


---

### `poll_option`

| Field         | Type | Constraints             | Description                     |
| ------------- | ---- | ----------------------- | ------------------------------- |
| `poll_id`     | UUID | **FK** → `poll.poll_id` | Parent poll (cascade delete)    |
| `option_text` | TEXT | NOT NULL                | Answer label                    |
| `position`    | INT  | DEFAULT `0`             | Display order                   |
| `vote_count`  | INT  | DEFAULT `0`             | Denormalised counter (optional) |

---

### `vote`

| Field              | Type        | Constraints                      | Description          |
| ------------------ | ----------- | -------------------------------- | -------------------- |
| `poll_id`          | UUID        | **FK** → `poll.poll_id`          | Associated poll      |
| `option_id`        | UUID        | **FK** → `poll_option.option_id` | Selected option      |
| `user_id`          | UUID        | NULL, FK → `user.user_id`        | Authenticated voter  |
| `voter_identifier` | VARCHAR(64) | NULL                             | Cookie / device hash |


---

## Enumerations

| Enum              | Allowed Values                                      | Purpose              |
|-------------------|-----------------------------------------------------|----------------------|
| `user_role`       | `'user'`, `'admin'`                                 | Basic RBAC           |
| `vote_protection` | `'ip'`, `'cookie'`, `'session'`, `'user'`, `'none'` | Duplicate-vote guard |


---

## Future Extensions

1. Additional question types – e.g. dates
2. Add ability that "guests" can enter a name when voting, so they don't have to register, and a vote is not anonymous