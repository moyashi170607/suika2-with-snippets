/* -*- coding: utf-8; tab-width: 8; indent-tabs-mode: t; -*- */

/*
 * Suika2 / Polaris Engine
 * Copyright (C) 2001-2016, Keiichi Tabata. All rights reserved.
 */

#ifndef SUIKA_ASOUND_H
#define SUIKA_ASOUND_H

/* ALSAの初期化処理を行う */
bool init_asound(void);

/* ALSAの終了処理を行う */
void cleanup_asound(void);

#endif
