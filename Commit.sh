#!/bin/bash

ORIGINAL_DIR="$(pwd)"

# -------------------------------
# 1. Получаем сообщение коммита через WinForms
COMMIT_MSG=$(powershell -NoProfile -Command "
Add-Type -AssemblyName System.Windows.Forms
\$form = New-Object System.Windows.Forms.Form
\$form.Text = 'Git Commit'
\$form.Size = New-Object System.Drawing.Size(400,150)
\$form.StartPosition = 'CenterScreen'

\$textBox = New-Object System.Windows.Forms.TextBox
\$textBox.Size = New-Object System.Drawing.Size(360,20)
\$textBox.Location = New-Object System.Drawing.Point(10,20)
\$form.Controls.Add(\$textBox)

\$okButton = New-Object System.Windows.Forms.Button
\$okButton.Text = 'OK'
\$okButton.Location = New-Object System.Drawing.Point(220,60)
\$okButton.DialogResult = [System.Windows.Forms.DialogResult]::OK
\$form.Controls.Add(\$okButton)

\$cancelButton = New-Object System.Windows.Forms.Button
\$cancelButton.Text = 'Cancel'
\$cancelButton.Location = New-Object System.Drawing.Point(300,60)
\$cancelButton.DialogResult = [System.Windows.Forms.DialogResult]::Cancel
\$form.Controls.Add(\$cancelButton)

\$form.AcceptButton = \$okButton
\$form.CancelButton = \$cancelButton

if (\$form.ShowDialog() -eq [System.Windows.Forms.DialogResult]::OK) { \$textBox.Text } else { exit 1 }
")

# Если пользователь нажал Cancel или закрыл окно
if [ $? -ne 0 ] || [ -z "$COMMIT_MSG" ]; then
    echo "Коммит отменён."
    exit 0
fi

# -------------------------------
# 2. Обновляем версии через UpdateVersion.sh
sh "Other/Other/GitHook/UpdateVersion.sh"

# -------------------------------
# 3. Создаём временный файл-флаг для pre-commit
FLAG_FILE="TempCommit.txt"
GIT_ROOT=$(git rev-parse --show-toplevel)
cd "$GIT_ROOT" || exit 1
touch "$FLAG_FILE"

# -------------------------------
# 4. Возвращаемся в исходную папку
cd "$ORIGINAL_DIR" || exit 1

# 5. Добавляем все изменения и делаем коммит
git add -A
git commit -m "$COMMIT_MSG"

echo "Коммит выполнен: $COMMIT_MSG"