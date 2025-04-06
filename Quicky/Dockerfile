FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base

USER root

RUN apt-get update && \
    apt-get install -y --allow-unauthenticated \
        tesseract-ocr \
        tesseract-ocr-rus \
        libleptonica-dev \
        libtesseract-dev \
    && rm -rf /var/lib/apt/lists/*

# Проверяем, какие каталоги существуют в /usr/lib/
RUN ls -l /usr/lib/

# Ищем библиотеки по всему файловому дереву
RUN find / -name "libdl*"
RUN find / -name "libleptonica*"
RUN find / -name "libtesseract*"

WORKDIR /app/x64

# Пробуем создать символические ссылки, если библиотеки найдены
RUN ln -s /usr/lib/x86_64-linux-gnu/liblept.so.5 /app/x64/libleptonica-1.82.0.so || true
RUN ln -s /usr/lib/x86_64-linux-gnu/libtesseract.so.4 /app/x64/libtesseract50.so || true

RUN apt-get clean && rm -rf /var/lib/apt/lists/*

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

COPY tessdata /usr/share/tesseract-ocr/4.00/tessdata
ENV TESSDATA_PREFIX=/usr/share/tesseract-ocr/4.00/tessdata

RUN ldconfig -p

ENTRYPOINT ["dotnet", "Quickly.dll"]
